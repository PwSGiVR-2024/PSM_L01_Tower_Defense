using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSpot : MonoBehaviour
{
    [Header("UI Menu Obiektu")]
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private GameObject hoverIndicator;

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

                // Przypisz aktywne miejsce budowy do ka¿dego buildera
                foreach (var builder in buildMenu.GetComponentsInChildren<tower_builder>())
                    builder.SetActiveBuildSpot(transform);

                foreach (var builder in buildMenu.GetComponentsInChildren<tower_builder_1>())
                    builder.SetActiveBuildSpot(transform);

                foreach (var builder in buildMenu.GetComponentsInChildren<tower_builder_2>())
                    builder.SetActiveBuildSpot(transform);
            }
        }
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
