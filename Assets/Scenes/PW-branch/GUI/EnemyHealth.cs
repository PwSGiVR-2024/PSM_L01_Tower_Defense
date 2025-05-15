using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        originalColor = rend.material.color;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        StartCoroutine(DamageFlash());

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DamageFlash()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        rend.material.color = originalColor;
    }
}
