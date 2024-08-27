using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class QuickPlay : MonoBehaviour
{
    private MusicManager musicManager;

    private void OnEnable()
    {
        ClickMeAnimation();
    }

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    public void OnQuickPlayPressed()
    {
        LevelEvents.OnLevelSelected?.Invoke(0);
    }

    void ClickMeAnimation()
    {
        DOTween.Sequence()
            .Append(transform.DOPunchScale(Vector3.one * .15f, .5f).SetEase(Ease.InOutExpo))
            .AppendInterval(.3f)
            .SetLoops(-1, LoopType.Restart)
            .OnKill(() => { transform.localScale = Vector3.one; })
            .SetId(transform);
    }
}