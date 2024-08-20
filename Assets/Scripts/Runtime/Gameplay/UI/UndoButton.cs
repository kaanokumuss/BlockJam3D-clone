using UnityEngine;
using UnityEngine.UI;

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
        submitManager.UndoLastMove(); 
    }
}