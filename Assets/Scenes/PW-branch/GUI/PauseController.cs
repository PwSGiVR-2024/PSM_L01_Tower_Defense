using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel; // Przypisz w inspektorze swój panel pauzy

    private bool isPaused = false;

    void Start()
    {
        // Panel pauzy jest niewidoczny na pocz¹tku gry
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
        pausePanel.SetActive(true);    // Poka¿ panel pauzy
        Time.timeScale = 0f;           // Zatrzymaj grê
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);   // Ukryj panel pauzy
        Time.timeScale = 1f;           // Wznow grê
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;           // Upewnij siê, ¿e czas jest normalny przed zmian¹ sceny
        SceneManager.LoadScene("Main_Menu"); // Podaj nazwê swojej sceny menu g³ównego
    }

    public void OpenOptions()
    {
        // Tutaj mo¿esz dodaæ logikê otwarcia panelu opcji
        Debug.Log("Opcje - tutaj dodaj logikê");
    }

    public void QuitGame()
    {
        Debug.Log("Wyjœcie z gry");
        Application.Quit();
    }
}
