using UnityEngine;

public class build_controler : MonoBehaviour
{
    private GameObject highlight;
    private GameObject menu;

    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Tower_Menu");
        highlight = GameObject.FindGameObjectWithTag("highlight");

        if (highlight != null)
        {
            highlight.SetActive(false); // Ukryj highlight na start
        }
        else
        {
            Debug.LogError("Nie znaleziono obiektu 'highlight' w scenie!");
        }
        if (menu != null)
        {
            menu.SetActive(false); // Ukryj highlight na start
        }
        else
        {
            Debug.LogError("Nie znaleziono obiektu 'highlight' w scenie!");
        }
    }

    void OnMouseOver()
    {
        if (highlight != null)
        {
            highlight.SetActive(true); // Poka¿ obiekt po najechaniu
        }
    }

    void OnMouseExit()
    {
        if (highlight != null)
        {
            highlight.SetActive(false); // Ukryj obiekt po odjechaniu mysz¹
        }
    }

    void OnMouseDown()
    {
        if (menu != null)
        {
            menu.SetActive(true); // Poka¿ obiekt po najechaniu
        }
    }
}
