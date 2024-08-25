using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MatchManager : MonoBehaviour
{
    [SerializeField] SubmitManager submitManager;
    [SerializeField] private SphereMoveController sphereMoveController;
    [SerializeField] ScoreManager scoreManager;
    private int requiredCount = 3;
    public void CheckForMatchingMaterials()
    {
        submitManager.isCheckingForMatch = true;

        Dictionary<string, List<GameObject>> materialGroups = new Dictionary<string, List<GameObject>>();
        foreach (var info in submitManager.sphereInfos)
        {
            string materialName = info.Material.name;

            if (!materialGroups.ContainsKey(materialName))
            {
                materialGroups[materialName] = new List<GameObject>();
            }
            materialGroups[materialName].Add(info.SphereObject);
        }
        
        foreach (var materialGroup in materialGroups)
        {
            if (materialGroup.Value.Count >= requiredCount)
            {
                Sequence sequence = DOTween.Sequence();
                foreach (var SphereObject in materialGroup.Value)
                {
                    sequence.Join(SphereObject.transform.DOScale(Vector3.zero, 0.5f));
                }

                sequence.OnComplete(() =>
                {
                    RemoveMatchingSpheres(materialGroup.Key);
                    ScoreEvents.OnDestroyedSphere?.Invoke();
                    RearrangeSpheres();
                    submitManager.isCheckingForMatch = false;
                });

                sequence.Play();
                return;
            }
        }

        submitManager.isCheckingForMatch = false;
    }

    void RemoveMatchingSpheres(string materialName)
    {
        for (int i = submitManager.sphereInfos.Count - 1; i >= 0; i--)
        {
            if (submitManager.sphereInfos[i].Material.name == materialName)
            {
                Destroy(submitManager.sphereInfos[i].SphereObject);
                submitManager.sphereInfos.RemoveAt(i);
            }
        }
        NoExistingSphere();
    } 
    public void RearrangeSpheres()
    {
        submitManager.sphereInfos.Sort((a, b) => a.Index.CompareTo(b.Index));
   
        for (int i = 0; i < submitManager.sphereInfos.Count; i++)
        {
            submitManager.sphereInfos[i].Index = i;
            Vector3 newPosition =submitManager.submitPositions[i].position;
            newPosition.y += 0.46f;
            submitManager.sphereInfos[i].SphereObject.GetComponent<Sphere>().MoveToWithAgent(newPosition);
        }
    } 
    public bool IsPositionAvailable(int index)
    {
        // 1. Adım: İndeksin geçerli bir aralıkta olup olmadığını kontrol edin
        if (index < 0 || index >= submitManager.submitPositions.Length)
        {
            // İndeks negatif veya dizinin uzunluğundan büyük veya eşitse, pozisyon geçerli değil
            return false;
        }

        // 2. Adım: sphereInfos listesindeki her bir elemanı kontrol edin
        foreach (var info in submitManager.sphereInfos)
        {
            // Eğer mevcut sphereInfos elemanının indeksi kontrol edilen indeksle eşleşiyorsa
            if (info.Index == index)
            {
                return false;
            }
        }

        return true;
    }

    public int GetAvailableIndexForMaterial(Material material)
    {
        List<int> sameMaterialIndices = new List<int>();
        foreach (var info in submitManager.sphereInfos)
        {
            if (info.Material.name == material.name)
            {
                sameMaterialIndices.Add(info.Index);
            }
        }

        if (sameMaterialIndices.Count > 0)
        {
            int lastSameMaterialIndex = sameMaterialIndices[sameMaterialIndices.Count - 1];
            int nextIndex = lastSameMaterialIndex + 1;

            if (IsPositionAvailable(nextIndex))
            {
                return nextIndex;
            }
            else
            {
                // Pozisyon mevcut değilse, küreleri sağa kaydırın
                sphereMoveController.ShiftSpheresRight(nextIndex);
            
                // shift işlemi başarılı olup olmadığını kontrol edin ve ona göre hareket edin
                if (IsPositionAvailable(nextIndex))
                {
                    return nextIndex;
                }
            }
        }

        // Tüm pozisyonları kontrol edin
        for (int i = 0; i < submitManager.submitPositions.Length; i++)
        {
            if (IsPositionAvailable(i))
            {
                return i;
            }
        }

        // Hiçbir pozisyon mevcut değilse, -1 döndürün
        return -1;
    }

    // public bool IsPositionAvailable(int index)
    // {
    //     // 1. Adım: İndeksin geçerli bir aralıkta olup olmadığını kontrol edin
    //     if (index < 0 || index >= submitManager.submitPositions.Length)
    //     {
    //         // İndeks negatif veya dizinin uzunluğundan büyük veya eşitse, pozisyon geçerli değil
    //         return false;
    //     }
    //
    //     // 2. Adım: sphereInfos listesindeki her bir elemanı kontrol edin
    //     foreach (var info in submitManager.sphereInfos)
    //     {
    //         // Eğer mevcut sphereInfos elemanının indeksi kontrol edilen indeksle eşleşiyorsa
    //         if (info.Index == index)
    //         {
    //             return false;
    //         }
    //     }
    //
    //     return true;
    // }
    // public int GetAvailableIndexForMaterial(Material material)
    // {
    //     
    //     List<int> sameMaterialIndices = new List<int>();
    //     foreach (var info in submitManager.sphereInfos)
    //     {
    //         if (info.Material.name == material.name)
    //         {
    //             sameMaterialIndices.Add(info.Index);
    //         }
    //     }
    //
    //     if (sameMaterialIndices.Count > 0)
    //     {
    //         int lastSameMaterialIndex = sameMaterialIndices[sameMaterialIndices.Count - 1];
    //         int nextIndex = lastSameMaterialIndex + 1;
    //
    //         if (IsPositionAvailable(nextIndex))
    //         {
    //             return nextIndex;
    //         }
    //         else
    //         {
    //             sphereMoveController.ShiftSpheresRight(nextIndex);
    //             return nextIndex;
    //         }
    //     }
    //
    //     for (int i = 0; i < submitManager.submitPositions.Length; i++)
    //     {
    //         if (IsPositionAvailable(i))
    //         {
    //             return i;
    //         }
    //     }
    //
    //     return -1;
    // }

    void NoExistingSphere()
    {
        if (submitManager.sphereInfos.Count == 0)
        {
            GameEvents.OnWin?.Invoke();
        }
    }
}

