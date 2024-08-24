using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MatchManager : MonoBehaviour
{
    [SerializeField] SubmitManager submitManager;
    [SerializeField] private SphereMoveController sphereMoveController;
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
        return index >= 0 && index < submitManager.submitPositions.Length && !submitManager.sphereInfos.Exists(info => info.Index == index);
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
                sphereMoveController.ShiftSpheresRight(nextIndex);
                return nextIndex;
            }
        }

        for (int i = 0; i < submitManager.submitPositions.Length; i++)
        {
            if (IsPositionAvailable(i))
            {
                return i;
            }
        }

        return -1;
    }
}

