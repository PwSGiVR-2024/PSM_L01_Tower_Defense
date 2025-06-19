using UnityEngine;
using TMPro;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance { get; private set; }

    [SerializeField] private int startingMoney = 500;
    private int currentMoney;

    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentMoney = startingMoney;
            UpdateUI();
            Debug.Log("PlayerWallet initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnEnemyKilled += AddMoney;
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
            EventManager.Instance.OnEnemyKilled -= AddMoney;
    }

    public bool SpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            EventManager.Instance?.MoneyChanged(currentMoney);
            UpdateUI();
            Debug.Log($"Wydano {amount}, pozosta³o: {currentMoney}");
            return true;
        }

        Debug.Log("Nie masz wystarczaj¹co pieniêdzy!");
        EventManager.Instance?.NotEnoughMoney();
        return false;
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        EventManager.Instance?.MoneyChanged(currentMoney);
        UpdateUI();
        Debug.Log($"Dodano {amount}, nowe saldo: {currentMoney}");
    }

    private void UpdateUI()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Money: {currentMoney}";
        }
        else
        {
            Debug.LogWarning("moneyText nie przypisany!");
        }
    }
}
