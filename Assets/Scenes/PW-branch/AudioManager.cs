using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    [Header("Ustawienia dźwięków")]
    public AudioClip buttonClickSound;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Dodajemy dwa AudioSource - jeden na muzykę, drugi na efekty
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
    }

    // MUZYKA
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip != clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // EFEKTY DŹWIĘKOWE
    public void PlayButtonClick()
    {
        if (buttonClickSound != null)
        {
            sfxSource.PlayOneShot(buttonClickSound, 1f);
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
