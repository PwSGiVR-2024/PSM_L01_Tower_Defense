using UnityEngine;
using UnityEngine.UI;

public class AudioMuteToggle : MonoBehaviour
{
    public GameObject muteButton;   // przypisz przycisk Mute
    public GameObject unmuteButton; // przypisz przycisk Unmute

    private bool isMuted = false;

    void Start()
    {
        UpdateButtons();
    }



    public void MuteAudio()
    {
        isMuted = true;
        AudioManager.instance.SetMusicVolume(0f);
        AudioManager.instance.SetSFXVolume(0f);
        UpdateButtons();
    }

    public void UnmuteAudio()
    {
        isMuted = false;
        AudioManager.instance.SetMusicVolume(1f); // lub inna wartoœæ, któr¹ chcesz domyœlnie
        AudioManager.instance.SetSFXVolume(1f);
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        muteButton.SetActive(!isMuted);
        unmuteButton.SetActive(isMuted);
    }

}
