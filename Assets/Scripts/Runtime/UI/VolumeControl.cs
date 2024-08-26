using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private AudioSource musicAudioSource;
    private const string VolumeKey = "MusicVolume";

    void Start()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.SetVolumeSlider(volumeSlider);
        }
        else
        {
            Debug.LogWarning("MusicManager bulunamad�!");
        }
        musicAudioSource = FindObjectOfType<MusicManager>().GetComponent<AudioSource>();

        // Slider'�n maksimum ve minimum de�erlerini belirleyin
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;

        // �nceki ayar� y�kleyin veya varsay�lan de�eri kullan�n
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        volumeSlider.value = savedVolume;
        musicAudioSource.volume = savedVolume;

        // Slider'�n de�eri de�i�ti�inde ses seviyesini ayarlay�n
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        musicAudioSource.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
    }
}
