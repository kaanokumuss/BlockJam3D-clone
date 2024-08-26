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
        // MusicManager'i sahnede bul ve referans olarak sakla
        musicManager = FindObjectOfType<MusicManager>();
    }

    public void OnQuickPlayPressed()
    {
        if (musicManager != null && musicManager.GetComponent<AudioSource>().isPlaying)
        {
            // Burada ekstra bir i�lem yapmam�za gerek yok, m�zik zaten devam edecek
            Debug.Log("MusicManager mevcut ve m�zik devam ediyor.");
        }
        else
        {
            Debug.LogError("MusicManager bulunamad�! M�zik kesilebilir.");
        }

        // LevelManager'daki ilgili fonksiyonu tetiklemek i�in level indexini 0 olarak belirtiyoruz
        LevelEvents.OnLevelSelected?.Invoke(0);

        // Oyun sahnesini y�kl�yoruz
        //SceneManager.LoadScene("GameScene");
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

    
}
