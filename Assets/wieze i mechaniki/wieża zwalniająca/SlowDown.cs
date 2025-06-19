using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SlowDown : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float slowFactor = 0.5f;
    [SerializeField] private float slowDuration = 3f;
    [SerializeField] private float lifetime = 10f;
    public LayerMask enemyLayer;

    private Transform target;

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // SprawdŸ, czy to przeciwnik i czy to nasz cel
        if (((1 << other.gameObject.layer) & enemyLayer) == 0) return;
        if (other.transform != target) return;

        Debug.Log($"Slow pocisk trafi³ {other.name} i wybucha!");

        ExplodeSlow();
        Destroy(gameObject);
    }

    private void ExplodeSlow()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
        int slowedCount = 0;

        foreach (Collider hit in colliders)
        {
            NavMeshAgent agent = hit.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                StartCoroutine(SlowAgent(agent));
                slowedCount++;
            }
        }

        Debug.Log($"Spowolnionych przeciwników: {slowedCount}");
    }

    private IEnumerator SlowAgent(NavMeshAgent agent)
    {
        float originalSpeed = agent.speed;
        agent.speed = originalSpeed * slowFactor;

        yield return new WaitForSeconds(slowDuration);

        // Przywróæ prêdkoœæ, ale tylko jeœli agent nadal istnieje
        if (agent != null)
        {
            agent.speed = originalSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
