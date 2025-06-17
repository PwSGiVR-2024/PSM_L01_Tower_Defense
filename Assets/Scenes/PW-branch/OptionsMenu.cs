using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; // je�li chcesz u�y� AudioMixer (opcjonalnie)
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Dropdown qualityDropdown;

    private Resolution[] resolutions;

    void Start()
    {
        // --- G�o�no�� ---
        if (AudioManager.instance != null)
        {
            musicSlider.value = AudioManager.instance.GetMusicVolume();
            sfxSlider.value = AudioManager.instance.GetSFXVolume();

            SetMusicVolume(musicSlider.value);
            SetSFXVolume(sfxSlider.value);

            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        // --- Rozdzielczo�ci ---
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // --- Fullscreen ---
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // --- Jako�� grafiki ---
        qualityDropdown.ClearOptions();
        List<string> qualityOptions = new List<string>();
        int currentQualityIndex = QualitySettings.GetQualityLevel();

        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            qualityOptions.Add(QualitySettings.names[i]);
        }

        qualityDropdown.AddOptions(qualityOptions);
        qualityDropdown.value = currentQualityIndex;
        qualityDropdown.RefreshShownValue();

        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    // --- G�o�no�� ---
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

    // --- Rozdzielczo�� ---
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }

    // --- Fullscreen ---
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // --- Jako�� ---
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
