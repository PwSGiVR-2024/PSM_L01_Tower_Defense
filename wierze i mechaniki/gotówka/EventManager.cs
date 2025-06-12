using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public event Action<int> OnEnemyKilled;
    public event Action<int> OnMoneyChanged;
    public event Action OnNotEnoughMoney;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("EventManager initialized.");
        }
        else
        {
            Debug.LogWarning("Drugi EventManager wykryty! Usuwam duplikat.");
            Destroy(gameObject);
        }
    }

    public void EnemyKilled(int reward)
    {
        OnEnemyKilled?.Invoke(reward);
    }

    public void MoneyChanged(int amount)
    {
        OnMoneyChanged?.Invoke(amount);
    }

    public void NotEnoughMoney()
    {
        OnNotEnoughMoney?.Invoke();
    }
}
