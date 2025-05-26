using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Metoda do resetowania poziomu (wczytuje aktualn¹ scenê od nowa)
    public void RestartLevel()
    {
        Time.timeScale = 1f; // Wznawiamy grê
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Metoda do powrotu do menu g³ównego (wczytuje scenê menu)
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Wznawiamy grê
        SceneManager.LoadScene("MainMenu"); // Podaj nazwê sceny menu g³ównego
    }

    // Metoda do wyjœcia z gry (dzia³a po buildzie)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game"); // Dla testów w edytorze
    }
}
