using UnityEngine;
using System.Collections;

public class DamageControler : MonoBehaviour
{
    [SerializeField] private int reward = 10;
    [SerializeField] private float Hp = 100f;
    [SerializeField] private float destroyDelay = 0f;
    public int damageToBase = 1;

    private bool isInvulnerable = false;
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        originalColor = rend.material.color;
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable) return;

        Debug.Log($"[DEBUG] Przeciwnik {gameObject.name} otrzymuje {damage} obra¿eñ");

        Hp -= damage;
        StartCoroutine(DamageFlash());

        if (Hp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invulnerability());
        }
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        Debug.Log("Przeciwnik jest nietykalny!");
        yield return new WaitForSeconds(0.1f);
        isInvulnerable = false;
        Debug.Log("Przeciwnik mo¿e znowu otrzymywaæ obra¿enia.");
    }

    private void Die()
    {
        Debug.Log("Przeciwnik zniszczony!");
        Destroy(gameObject, destroyDelay);

        if (EventManager.Instance != null)
        {
            EventManager.Instance.EnemyKilled(reward);
        }
    }

    IEnumerator DamageFlash()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        rend.material.color = originalColor;
    }


}
