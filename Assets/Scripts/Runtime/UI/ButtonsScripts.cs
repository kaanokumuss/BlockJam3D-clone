using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private Button button;

    [Header("Punch Animation Settings")] [SerializeField, Range(0.01f, 1f)]
    private float punchScale = 0.1f;

    [SerializeField, Range(0.1f, 2f)] private float punchDuration = 0.5f;

    void Start()
    {
        button.transform.DOPunchScale(Vector3.one * punchScale, punchDuration)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo)
            .SetId(button.transform);
    }

#if UNITY_EDITOR
    [Button]
    void FindButton()
    {
        button = GetComponent<Button>();
    }
#endif
}