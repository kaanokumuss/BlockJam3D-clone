using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UndoButton : MonoBehaviour
{
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

    void OnClick()
    {
        UndoLastMove(); 
        ScoreEvents.OnTappedUndoButton?.Invoke();

    }
    public void UndoLastMove()
    {
        if (submitManager.undoStack.Count > 0)
        {
            Sphere lastMovedSphere = submitManager.undoStack.Pop();
            Debug.Log("Popped");

            // Sphere'i undo işlemi için geri taşı
            lastMovedSphere.MoveBack().OnComplete(() =>
            {
                // Sphere'in info listesinden çıkarılması
                submitManager.sphereInfos.RemoveAll(info => info.SphereObject == lastMovedSphere.gameObject);
                submitManager.isCheckingForMatch = false;
            });
        }
    } 
}