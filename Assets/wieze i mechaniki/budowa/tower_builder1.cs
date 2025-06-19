using UnityEngine;

public class tower_builder_1 : MonoBehaviour
{
    [SerializeField] private int towerCost = 120;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private build_controller buildController;
    [SerializeField] private GameObject towerMenu;

    private Transform activeBuildSpot;

    public void SetActiveBuildSpot(Transform buildSpot)
    {
        activeBuildSpot = buildSpot;
    }

    public void BuildTower()
    {
        if (PlayerWallet.Instance != null && PlayerWallet.Instance.SpendMoney(towerCost))
        {
            if (buildController != null && activeBuildSpot != null)
            {
                Transform buildPosition = activeBuildSpot.Find("Tower_place");
                if (buildPosition != null)
                {
                    buildController.Build(towerPrefab, buildPosition);
                    Debug.Log("Wie¿a 1 zbudowana.");
                }
                else
                {
                    Debug.LogError("Brak obiektu 'tower_place' w " + activeBuildSpot.name);
                }
            }
            else
            {
                Debug.LogError("Brak build_controller albo aktywnego pola!");
            }

            if (towerMenu != null)
                towerMenu.SetActive(false);
        }
        else
        {
            Debug.Log("Za ma³o pieniêdzy.");
        }
    }
}
