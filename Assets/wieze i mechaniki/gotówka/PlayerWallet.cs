using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance { get; private set; }

    [SerializeField] private int startingMoney = 500;
    private int currentMoney;

    [SerializeField] private Text moneyText;

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

    private void Start()
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
            Debug.Log($"Wydano {amount}, pozostało: {currentMoney}");
            return true;
        }

        Debug.Log("Nie masz wystarczająco pieniędzy!");
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
            moneyText.text = currentMoney.ToString();
        }
        else
        {
            Debug.LogWarning("moneyText nie przypisany!");
        }
    }
}
