using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float baseSpeed = 5f;

    private NavMeshAgent agent;
    private Renderer rend;
    private Color originalColor;

    private Coroutine slowCoroutine;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;

        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
            originalColor = rend.material.color;
    }

    void Update()
    {
        // Jeœli masz ustawiony destination gdzie indziej, to nic tu nie rób
        // Jeœli chcesz testowo, to np. poruszaj siê do przodu:
        // agent.destination = transform.position + transform.forward * 10f;
    }

    public void ApplySlow(float slowFactor, float duration)
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            ResetSpeed();
        }

        slowCoroutine = StartCoroutine(SlowEffect(slowFactor, duration));
    }

    IEnumerator SlowEffect(float slowFactor, float duration)
    {
        if (agent != null)
        {
            agent.speed = baseSpeed * slowFactor;
        }

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = Mathf.PingPong(Time.time * 3f, 1f);
            if (rend != null)
                rend.material.color = Color.Lerp(originalColor, Color.blue, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        ResetSpeed();
    }

    private void ResetSpeed()
    {
        if (agent != null)
            agent.speed = baseSpeed;

        if (rend != null)
            rend.material.color = originalColor;
    }
}
