using UnityEngine;

public class VictoryController : MonoBehaviour
{
    public GameObject victoryPanel; // Przypisz w inspektorze panel z video i przyciskami

    void Start()
    {
        // Ukrywamy panel na start
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    // Wywo³aj tê metodê, gdy chcesz pokazaæ ekran zwyciêstwa
    public void ShowVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0f; // Pauzujemy grê
        }
    }

    // Metody do przycisków

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu"); // lub nazwa Twojej sceny menu
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}
