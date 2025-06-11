using UnityEngine;
using TMPro;

public class build_controller : MonoBehaviour
{
    [SerializeField] private int towerCost = 100;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Transform buildPosition;

    private void Start()
    {
        Debug.Log("build_controller gotowy.");
    }

    public void BuildTower()
    {
        Debug.Log("BuildTower() klikniête.");
        if (PlayerWallet.Instance != null && PlayerWallet.Instance.SpendMoney(towerCost))
        {
            if (towerPrefab != null && buildPosition != null)
            {
                Instantiate(towerPrefab, buildPosition.position, Quaternion.identity);
                Debug.Log("Wie¿a zbudowana!");
            }
            else
            {
                Debug.LogError("Nie przypisano towerPrefab lub buildPosition.");
            }
        }
        else
        {
            Debug.Log("Brak pieniêdzy na wie¿ê.");
        }
    }
}
