using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    private float normalSpeed;

    private Renderer rend;
    private Color originalColor;

    private Coroutine slowCoroutine;

    void Start()
    {
        normalSpeed = speed;
        rend = GetComponentInChildren<Renderer>();
        originalColor = rend.material.color;
    }

    void Update()
    {
        // Prosty ruch do przodu
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void ApplySlow(float slowFactor, float duration)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            rend.material.color = originalColor; // reset koloru
            speed = normalSpeed;
        }
        slowCoroutine = StartCoroutine(SlowEffect(slowFactor, duration));
    }

    IEnumerator SlowEffect(float slowFactor, float duration)
    {
        speed = normalSpeed * slowFactor;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.PingPong(Time.time * 3f, 1f);
            rend.material.color = Color.Lerp(originalColor, Color.blue, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
        rend.material.color = originalColor;
        speed = normalSpeed;
    }
}
