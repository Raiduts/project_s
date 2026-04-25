using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public float musicVolume, sfxVolume;

    [Header("Audio Sources")]
    public AudioMixer audioMixer;
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Clips")]
    public AudioClip buttonClick;
    public AudioClip hover;
    public AudioClip success;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("music");
        sfxVolume = PlayerPrefs.GetFloat("sfx");

        SetMusicVolume(musicVolume);
        SetSfxVolume(sfxVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;

        volume = Mathf.Clamp(volume, 0.0001f, 1f);

        float dB = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("Music_Volume", dB);

        PlayerPrefs.SetFloat("music", volume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;

        volume = Mathf.Clamp(volume, 0.0001f, 1f);

        float dB = Mathf.Log10(volume) * 20;
        audioMixer.SetFloat("Sfx_Volume", dB);

        PlayerPrefs.SetFloat("sfx", volume);
    }

    public void PlayBGM(AudioClip clip, float duration)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        FadeBGM(musicSource.clip, duration);
    }

    public void PlaySFX(SFXType type)
    {
        switch (type)
        {
            case SFXType.ButtonClick01:
                sfxSource.PlayOneShot(buttonClick);
                break;
        }
    }

    public void FadeBGM(AudioClip newClip, float duration)
    {
        musicSource.DOFade(0, duration).OnComplete(() =>
        {
            musicSource.clip = newClip;
            musicSource.Play();
            musicSource.DOFade(1, duration);
        });
    }
}

public enum SFXType
{
    ButtonClick01,
    ButtonClick02,
    Success
}