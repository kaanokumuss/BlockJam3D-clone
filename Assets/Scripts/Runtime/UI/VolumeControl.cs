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

        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;

        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        volumeSlider.value = savedVolume;
        musicAudioSource.volume = savedVolume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float value)
    {
        musicAudioSource.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
    }
}