using DG.Tweening;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
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