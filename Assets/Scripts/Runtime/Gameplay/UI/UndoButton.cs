using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro'yu kullanmak için

public class UndoButton : MonoBehaviour
{
    [SerializeField] private MatchManager matchManager;
    [SerializeField] private SubmitManager submitManager;
    [SerializeField] private Button undoButton;
    [SerializeField] private TextMeshProUGUI remainingAttemptsText; // TMP referansı

    private bool hasUndone = false;
    [SerializeField] int remainingAttempts = 1; 

    private void OnEnable()
    {
        undoButton.onClick.AddListener(OnClick);
        UpdateRemainingAttemptsText();
    }

    private void OnDisable()
    {
        undoButton.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (!hasUndone && remainingAttempts > 0)
        {
            UndoLastMove();
            hasUndone = true;
            remainingAttempts--; 
            UpdateRemainingAttemptsText(); 
        }
    }

    public void UndoLastMove()
    {
        if (submitManager.undoStack.Count > 0)
        {
            Sphere lastMovedSphere = submitManager.undoStack.Pop();
            Debug.Log("Popped");

            lastMovedSphere.MoveBack(() =>
            {
                submitManager.sphereInfos.RemoveAll(info => info.SphereObject == lastMovedSphere.gameObject);
                submitManager.isCheckingForMatch = false;
                matchManager.RearrangeSpheres();
                ScoreEvents.OnTappedUndoButton?.Invoke();
            });
        }
    }

    private void UpdateRemainingAttemptsText()
    {
        if (remainingAttemptsText != null)
        {
            remainingAttemptsText.text = remainingAttempts.ToString();
        }
    }
}