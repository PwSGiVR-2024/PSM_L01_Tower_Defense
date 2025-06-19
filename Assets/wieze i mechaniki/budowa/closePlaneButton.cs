using UnityEngine;
using UnityEngine.UI;

public class closePlanelButton : MonoBehaviour
{
    [SerializeField] private GameObject panelToClose;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null && panelToClose != null)
        {
            btn.onClick.AddListener(ClosePanel);
        }
    }

    private void ClosePanel()
    {
        panelToClose.SetActive(false);
        Debug.Log($"{panelToClose.name} zosta³ zamkniêty.");
    }
}
