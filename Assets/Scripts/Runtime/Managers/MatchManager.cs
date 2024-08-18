using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MatchManager : MonoBehaviour
{
    [SerializeField] SubmitManager submitManager;

    private int requiredCount = 3;
    public void CheckForMatchingColors()
    {
        submitManager.isCheckingForMatch = true;
   
        Dictionary<Color, List<GameObject>> colorGroups = new Dictionary<Color, List<GameObject>>();
        foreach (var info in submitManager.sphereInfos)
        {
            if (!colorGroups.ContainsKey(info.Color))
            {
                colorGroups[info.Color] = new List<GameObject>();
            }
            colorGroups[info.Color].Add(info.SphereObject);
        }
   
        foreach (var colorGroup in colorGroups)
        {
            if (colorGroup.Value.Count >= requiredCount)
            {
                Sequence sequence = DOTween.Sequence();
                foreach (var SphereObject in colorGroup.Value)
                {
                    sequence.Join(SphereObject.transform.DOScale(Vector3.zero, 0.5f));
                }
   
                sequence.OnComplete(() =>
                {
                    RemoveMatchingSpheres(colorGroup.Key);
                    RearrangeSpheres();
                    submitManager.isCheckingForMatch = false;
                });
   
                sequence.Play();
                return;
            }
        }
   
        submitManager.isCheckingForMatch = false;
    }
   
    void RemoveMatchingSpheres(Color color)
    {
        for (int i = submitManager.sphereInfos.Count - 1; i >= 0; i--)
        {
            if (submitManager.sphereInfos[i].Color == color)
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

