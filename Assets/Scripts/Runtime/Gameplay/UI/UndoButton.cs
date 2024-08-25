using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UndoButton : MonoBehaviour
{
    [SerializeField] private MatchManager matchManager;
    [SerializeField] private SubmitManager submitManager;
    [SerializeField] private Button undoButton;

    private void OnEnable()
    {
        undoButton.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        undoButton.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        UndoLastMove();     
    }

    public void UndoLastMove()
    {
        if (submitManager.undoStack.Count > 0)
        {
            Sphere lastMovedSphere = submitManager.undoStack.Pop();
            Debug.Log("Popped");

            // Move the sphere back and pass a callback to handle completion
            lastMovedSphere.MoveBack(() =>
            {
                // Remove sphere from info list
                submitManager.sphereInfos.RemoveAll(info => info.SphereObject == lastMovedSphere.gameObject);
                submitManager.isCheckingForMatch = false;
                // Trigger score event
                matchManager.RearrangeSpheres();
                ScoreEvents.OnTappedUndoButton?.Invoke();
                
            });
        }
        // if (submitManager.undoStack.Count > 0)
        // {
        //     Sphere lastMovedSphere = submitManager.undoStack.Pop();
        //     Debug.Log("Popped");
        //
        //     // Move the sphere back and pass a callback to handle completion
        //     lastMovedSphere.MoveBack(() =>
        //     {
        //         // Remove sphere from info list
        //         submitManager.sphereInfos.RemoveAll(info => info.SphereObject == lastMovedSphere.gameObject);
        //         submitManager.isCheckingForMatch = false;
        //
        //         // Rearrange spheres after Undo
        //         matchManager.RearrangeSpheres();
        //
        //         // Trigger score event
        //         ScoreEvents.OnTappedUndoButton?.Invoke();
        //     });
        // }
    }
}