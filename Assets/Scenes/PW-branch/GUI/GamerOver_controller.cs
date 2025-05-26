using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Metoda do resetowania poziomu (wczytuje aktualn� scen� od nowa)
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Wznawiamy gr�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Metoda do powrotu do menu g��wnego (wczytuje scen� menu)
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Wznawiamy gr�
        SceneManager.LoadScene("MainMenu"); // Podaj nazw� sceny menu g��wnego
    }

    // Metoda do wyj�cia z gry (dzia�a po buildzie)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game"); // Dla test�w w edytorze
    }
}
