using UnityEngine;
using System.Collections;

public class DamageControler : MonoBehaviour
{
    [SerializeField] private float Hp = 100;
    [SerializeField] private string projectileTag = "Bullet";
    [SerializeField] private float destroyDelay = 0f;
    private bool isInvulnerable = false; // Flaga nietykalno�ci

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(projectileTag))
        {
            if (!isInvulnerable) // Sprawdzamy czy przeciwnik jest nietykalny
            {
                Bullet bullet = other.GetComponent<Bullet>();
                if (bullet != null)
                {
                    Debug.Log("Pocisk trafi� w przeciwnika!");
                    TakeDamage(bullet.GetDamage());
                }
            }
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable) return; // Je�li nietykalny, nie otrzymuje obra�e�

        Hp -= damage;
        Debug.Log($"Przeciwnik otrzyma� {damage} obra�e�. Pozosta�e HP: {Hp}");

        if (Hp <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invulnerability()); // Rozpocz�cie nietykalno�ci
        }
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        Debug.Log("Przeciwnik jest nietykalny!");
        yield return new WaitForSeconds(0.5f); // Czekaj 0.5 sekundy
        isInvulnerable = false;
        Debug.Log("Przeciwnik mo�e znowu otrzymywa� obra�enia.");
    }

    private void Die()
    {
        Debug.Log("Przeciwnik zniszczony!");
        Destroy(gameObject, destroyDelay);
    }
}
