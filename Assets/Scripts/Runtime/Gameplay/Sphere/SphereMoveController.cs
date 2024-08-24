using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class SphereMoveController : MonoBehaviour
{
    [SerializeField] private SubmitManager submitManager;
    [SerializeField] private MatchManager matchManager;
    public void ShiftSpheresRight(int startIndex)
    {
        for (int i = submitManager.sphereInfos.Count - 1; i >= 0; i--)
        {
            if (submitManager.sphereInfos[i].Index >= startIndex)
            {
                int newIndex = submitManager.sphereInfos[i].Index + 1;
                if (matchManager.IsPositionAvailable(newIndex))
                {
                    Vector3 newPosition = submitManager.submitPositions[newIndex].position;
                    newPosition.y += 0.46f;
                    submitManager.sphereInfos[i].MoveToWithAgent(newPosition);
                    submitManager.sphereInfos[i].Index = newIndex;
                }
                else
                {
                    Debug.LogError("No more space to shift spheres right!");
                    break;
                }
            }
        }
    }

    public void MoveSphereToPosition(GameObject sphere, int targetIndex, Material material)
    {
        Vector3 newPosition = submitManager.submitPositions[targetIndex].position;
        newPosition.y += 0.46f;
        
        sphere.GetComponent<Sphere>().MoveToWithAgent(newPosition, () =>
        {
            submitManager.undoStack.Push(sphere.GetComponent<Sphere>());
            submitManager.UpdateSphereInfo(sphere, targetIndex, material);
            matchManager.CheckForMatchingMaterials();
            submitManager.isCheckingForMatch = false;
        });
        
        
    }

    
}