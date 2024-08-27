using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Slider i�in gerekli

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    private AudioSource _audioSource;
    [SerializeField] private Slider volumeSlider;


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        _audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);


        if (_audioSource != null)
        {
            _audioSource.loop = true;

            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("AudioSource component not found on MusicManager!");
        }


        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        }


        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.SetVolumeSlider(volumeSlider);
        }
    }

    public void SetVolumeSlider(Slider slider)
    {
        volumeSlider = slider;

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        if (_audioSource != null && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }


    private void OnVolumeChanged(float value)
    {
        if (_audioSource != null)
        {
            _audioSource.volume = value;
            PlayerPrefs.SetFloat("Volume", value);
        }
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}