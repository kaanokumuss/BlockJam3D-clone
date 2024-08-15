using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class LevelsButton : MonoBehaviour
{
    [SerializeField] Button button;
    
    void OnEnable()
    {
        ClickMeAnimation();
        button.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    void ClickMeAnimation()
    {
        DOTween.Sequence()
            .Append(transform.DOPunchScale(Vector3.one * .15f, .5f).SetEase(Ease.InOutExpo))
            .AppendInterval(.3f)
            .SetLoops(-1, LoopType.Restart)
            .OnKill(() =>
            {
                transform.localScale = Vector3.one;
            })
            .SetId(transform);
    }

    void OnClick()
    {
        UIEvents.OpenLevelsPanel?.Invoke();
    }

#if UNITY_EDITOR
    [Button]
    void FindButton()
    {
        button = GetComponent<Button>();
    }
#endif

}
