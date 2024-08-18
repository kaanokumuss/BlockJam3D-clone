using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MatchManager : MonoBehaviour
{
    [SerializeField] SubmitManager submitManager;

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

        // Materyalleri ve ilgili oyun nesnelerini yazdır
        foreach (var materialGroup in materialGroups)
        {
            Debug.Log($"Material: {materialGroup.Key}, Count: {materialGroup.Value.Count}");
            foreach (var sphereObject in materialGroup.Value)
            {
                Debug.Log($" - Sphere Object: {sphereObject.name}");
            }
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

   
    
    void RearrangeSpheres()
    {
        submitManager.sphereInfos.Sort((a, b) => a.Index.CompareTo(b.Index));
   
        for (int i = 0; i < submitManager.sphereInfos.Count; i++)
        {
            submitManager.sphereInfos[i].Index = i;
            Vector3 newPosition =submitManager.submitPositions[i].position;
            newPosition.y += 0.46f;
            submitManager.sphereInfos[i].SphereObject.GetComponent<Sphere>().MoveTo(newPosition);
        }
    } 
}

