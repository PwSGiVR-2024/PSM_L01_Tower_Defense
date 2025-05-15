using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel; // Przypisz w inspektorze sw�j panel pauzy

    private bool isPaused = false;

    void Start()
    {
        // Panel pauzy jest niewidoczny na pocz�tku gry
        pausePanel.SetActive(false);
    }

    void Update()
    {
        // Pauza / wznowienie gry na klawisz Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);    // Poka� panel pauzy
        Time.timeScale = 0f;           // Zatrzymaj gr�
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);   // Ukryj panel pauzy
        Time.timeScale = 1f;           // Wznow gr�
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;           // Upewnij si�, �e czas jest normalny przed zmian� sceny
        SceneManager.LoadScene("Main_Menu"); // Podaj nazw� swojej sceny menu g��wnego
    }

    public void OpenOptions()
    {
        // Tutaj mo�esz doda� logik� otwarcia panelu opcji
        Debug.Log("Opcje - tutaj dodaj logik�");
    }

    public void QuitGame()
    {
        Debug.Log("Wyj�cie z gry");
        Application.Quit();
    }
}
