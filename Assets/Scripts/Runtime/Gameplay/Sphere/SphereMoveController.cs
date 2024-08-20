using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SphereMoveController : MonoBehaviour
{
    [SerializeField] private SubmitManager submitManager;
    [SerializeField] private MatchManager matchManager;
    public void ShiftSpheresRight(int startIndex)
    {
        for (int i = submitManager.sphereInfos.Count - 1; i >= 0; i--
             )
        {
            if (submitManager.sphereInfos[i].Index >= startIndex)
            {
                int newIndex = submitManager.sphereInfos[i].Index + 1;
                if (matchManager.IsPositionAvailable(newIndex))
                {
                    Vector3 newPosition = submitManager.submitPositions[newIndex].position;
                    newPosition.y += 0.46f;
                    submitManager.sphereInfos[i].MoveTo(newPosition);
                    submitManager.sphereInfos[i].Index = newIndex;
                }
                else
                {
                    Debug.LogError("No more space to shift spheres right!");
                    break;
                }
            }
        }
    } //SphereMoveController
    public void MoveSphereToPosition(GameObject sphere, int targetIndex, Material material)
    {
        Vector3 newPosition = submitManager.submitPositions[targetIndex].position;
        newPosition.y += 0.46f;
        // var spphere gget comp yaz
        
        sphere.GetComponent<Sphere>().MoveTo(newPosition).OnComplete(() =>
        {
            // Sphere nesnesini undo için sakla
            submitManager.undoStack.Push(sphere.GetComponent<Sphere>());

            submitManager.UpdateSphereInfo(sphere, targetIndex, material);
            matchManager.CheckForMatchingMaterials();
            submitManager.isCheckingForMatch = false;
        });
    } //SphereMoveController
}
