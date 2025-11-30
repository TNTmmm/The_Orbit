using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource buttonSFXSource;

    [Header("Background Music List (Per Scene or Usage)")]
    public AudioClip[] backgroundMusicList;

    [Header("Button Click SFX")]
    public AudioClip defaultButtonClick;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float sfxVolume = 1f;
    private const string SFX_VOLUME_KEY = "SFXVolume";

    public bool IsMusicOn { get; private set; }
    public bool IsSFXOn { get; private set; }
    public bool IsSFXEnabled => buttonSFXSource != null && buttonSFXSource.volume > 0f;



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudio();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void InitializeAudio()
    {
        if (musicSource == null) musicSource = gameObject.AddComponent<AudioSource>();
        if (buttonSFXSource == null) buttonSFXSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;

        // Load saved settings
        IsMusicOn = PlayerPrefs.GetInt("Music", 1) == 1;
        IsSFXOn = PlayerPrefs.GetInt("SFX", 1) == 1;
        sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f);

        UpdateVolume();

        // Play initial music
        if (backgroundMusicList != null && backgroundMusicList.Length > 0)
        {
            PlayMusic(backgroundMusicList[SceneManager.GetActiveScene().buildIndex]);
        }
    }

    private void UpdateVolume()
    {
        if (musicSource != null)
            musicSource.volume = IsMusicOn ? 1f : 0f;

        if (buttonSFXSource != null)
            buttonSFXSource.volume = sfxVolume * (IsSFXOn ? 1f : 0f);
    }

    // --- MUSIC ---
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null) return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void ToggleMusicBackground()
    {
        IsMusicOn = !IsMusicOn;
        UpdateVolume();

        PlayerPrefs.SetInt("Music", IsMusicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    // --- SFX ---
    public void PlaySFX(AudioClip clip)
    {
        if (!IsSFXOn || clip == null || buttonSFXSource == null) return;

        buttonSFXSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayClickSound()
    {
        PlaySFX(defaultButtonClick);
    }

    public void ToggleSFX()
    {
        IsSFXOn = !IsSFXOn;
        UpdateVolume();

        PlayerPrefs.SetInt("SFX", IsSFXOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        UpdateVolume();

        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
        PlayerPrefs.Save();
    }

    // --- Scene Switch Music ---
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int index = scene.buildIndex;

        if (backgroundMusicList != null && index < backgroundMusicList.Length)
        {
            PlayMusic(backgroundMusicList[index]);
        }
    }

}
