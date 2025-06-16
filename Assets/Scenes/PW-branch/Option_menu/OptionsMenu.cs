using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private void Start()
    {
        // Ustawienie slider�w na aktualne warto�ci z AudioManager
        musicVolumeSlider.value = AudioManager.instance != null ? AudioManager.instance.GetComponent<AudioSource>().volume : 1f;
        sfxVolumeSlider.value = AudioManager.instance != null ? AudioManager.instance.GetComponent<AudioSource>().volume : 1f;

        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetMusicVolume(volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetSFXVolume(volume);
        }
    }
}
