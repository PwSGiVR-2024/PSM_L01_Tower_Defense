using UnityEngine;
using UnityEngine.UI;

public class TriviaUIManager : MonoBehaviour
{
    public GameObject triviaPanel;
    public GameObject descriptionPanel;
    public Text descriptionText; // legacy Text
    public GameObject virusModelParent;

    public GameObject virusPrefab;
    public GameObject trojanPrefab;
    public GameObject ratPrefab;

    private GameObject currentModelInstance;

    public void ShowDescription(string type)
    {
        triviaPanel.SetActive(false);
        descriptionPanel.SetActive(true);

        if (currentModelInstance != null)
        {
            Destroy(currentModelInstance);
        }

        GameObject selectedPrefab = null;
        string description = "";

        switch (type.ToLower())
        {
            case "virus":
                selectedPrefab = virusPrefab;
                description = "Subject 0 (Virus): Z�o�liwy kod samoreplikuj�cy si�, infekuj�cy inne pliki w systemie.";
                break;
            case "trojan":
                selectedPrefab = trojanPrefab;
                description = "Subject 1 (Trojan): Ukrywa si� jako legalny plik, aby uzyska� dost�p do systemu u�ytkownika.";
                break;
            case "rat":
                selectedPrefab = ratPrefab;
                description = "Subject 2 (RAT): Zdalnie sterowany wirus umo�liwiaj�cy przej�cie kontroli nad komputerem.";
                break;
        }

        if (selectedPrefab != null)
        {
            currentModelInstance = Instantiate(selectedPrefab, virusModelParent.transform);
        }

        descriptionText.text = description;
    }

    public void ReturnToTrivia()
    {
        descriptionPanel.SetActive(false);
        triviaPanel.SetActive(true);

        if (currentModelInstance != null)
        {
            Destroy(currentModelInstance);
        }
    }

    public void ReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_Menu");
    }
}
