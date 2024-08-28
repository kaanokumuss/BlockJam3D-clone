using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public float waitTimeAwake = 5f;
    [SerializeField] private GameObject canvas;

    private static bool hasPlayed = false;

    void Awake()
    {
        if (!hasPlayed)
        {
            StartCoroutine(PlayAudioAfterDelay());
        }
        else
        {
            // Eğer daha önce oynatılmışsa, Canvas'ı gizle
            canvas.SetActive(false);
        }
    }

    IEnumerator PlayAudioAfterDelay()
    {
        yield return new WaitForSeconds(waitTimeAwake);
        audioSource.Play();

        // Canvas'ı aktif hale getir
        canvas.SetActive(true);

        // Ses bitene kadar bekle
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        // Ses bittiğinde Canvas'ı pasif hale getir
        canvas.SetActive(false);

        // Sesin oynatıldığını kaydet
        hasPlayed = true;
    }
}