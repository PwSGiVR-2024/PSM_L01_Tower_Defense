using UnityEngine;

public class tower_builder : MonoBehaviour
{
    [SerializeField] private int towerCost = 100;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Transform buildPosition;
    [SerializeField] private build_controller buildController;

    [SerializeField] private GameObject towerMenu;

    public void BuildTower()
    {
        if (PlayerWallet.Instance != null && PlayerWallet.Instance.SpendMoney(towerCost))
        {
            if (buildController != null)
            {
                buildController.Build(towerPrefab, buildPosition);
                Debug.Log("Budowa zakoñczona sukcesem.");
            }
            else
            {
                Debug.LogError("Brak przypisanego build_controller!");
            }

            if (towerMenu != null)
                towerMenu.SetActive(false);
        }
        else
        {
            Debug.Log("Nie masz wystarczaj¹cej iloœci pieniêdzy.");
        }
    }
}
