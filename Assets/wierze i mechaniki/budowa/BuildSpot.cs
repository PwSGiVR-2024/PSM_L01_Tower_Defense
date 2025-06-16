using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSpot : MonoBehaviour
{
    [Header("UI Menu Obiektu")]
    [SerializeField] private GameObject buildMenu;         // pe³ne menu budowy
    [SerializeField] private GameObject hoverIndicator;    // np. ikonka plusa

    private void Start()
    {
        if (buildMenu != null) buildMenu.SetActive(false);
        if (hoverIndicator != null) hoverIndicator.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (!IsPointerOverUI())
        {
            if (hoverIndicator != null)
                hoverIndicator.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (hoverIndicator != null)
            hoverIndicator.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!IsPointerOverUI())
        {
            if (buildMenu != null)
            {
                buildMenu.SetActive(true);
            }
        }
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}