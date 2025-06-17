using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject levelSelectPanel;
    public GameObject triviaPanel;
    public GameObject optionsPanel;

    public AudioClip mainMenuMusic; // Przypisz muzykê w Inspektorze

    void Start()
    {
        if (AudioManager.instance != null)
        {

        }
    }


    public void OpenLevelSelect()
    {
        PlayClick();
        levelSelectPanel.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        PlayClick();
        levelSelectPanel.SetActive(false);
    }

    public void LoadLevel(int levelNumber)
    {
        PlayClick();
        SceneManager.LoadScene("Level" + levelNumber);
    }

    public void OpenTrivia()
    {
        PlayClick();
        triviaPanel.SetActive(true);
    }

    public void CloseTrivia()
    {
        PlayClick();
        triviaPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        PlayClick();
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        PlayClick();
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        PlayClick();
        Application.Quit();
    }

    private void PlayClick()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayButtonClick();
        }
    }
}
