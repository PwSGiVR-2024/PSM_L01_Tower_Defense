using UnityEngine;
using System.Collections;

public class DamageControler : MonoBehaviour
{
    [SerializeField] private int reward = 10;
    [SerializeField] private float Hp = 100f;
    [SerializeField] private string projectileTag = "Bullet";
    [SerializeField] private float destroyDelay = 0f;

    private bool isInvulnerable = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(projectileTag))
        {
            if (!isInvulnerable)
            {
                Bullet bullet = other.GetComponent<Bullet>();
                if (bullet != null)
                {
                    Debug.Log("Pocisk trafi³ w przeciwnika!");
                    TakeDamage(bullet.GetDamage());
                }
            }
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable) return;

        Debug.Log($"[DEBUG] Przeciwnik {gameObject.name} otrzymuje {damage} obra¿eñ");
        Hp -= damage;

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
}
