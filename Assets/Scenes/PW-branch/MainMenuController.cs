using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Metoda do przejœcia do wyboru poziomu


    public GameObject levelSelectPanel;

    // Otwórz panel wyboru poziomu
    public void OpenLevelSelect()
    {
        levelSelectPanel.SetActive(true);
    }

    // Zamknij panel wyboru poziomu
    public void CloseLevelSelect()
    {
        levelSelectPanel.SetActive(false);
    }

    // Metoda do ³adowania sceny na podstawie numeru poziomu
    public void LoadLevel(int levelNumber)
    {
        // Zak³adamy, ¿e nazwy scen to np. "Level1", "Level2" itd.
        string sceneName = "Level" + levelNumber;
        SceneManager.LoadScene(sceneName);
    }


    // Metoda do otwarcia panelu Trivia
    public GameObject triviaPanel; // przypisz w inspektorze panel z informacjami o przeciwnikach

    public void OpenTrivia()
    {
        triviaPanel.SetActive(true);
    }

    public void CloseTrivia()
    {
        triviaPanel.SetActive(false);
    }

    // Metoda do otwarcia panelu Opcji
    public GameObject optionsPanel; // przypisz w inspektorze panel opcji

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    // Metoda do wyjœcia z gry
    public void QuitGame()
    {
        Debug.Log("Wyjœcie z gry");
        Application.Quit();
    }
}
