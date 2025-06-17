using UnityEngine;
using UnityEngine.UI;

public class AreaAttackController : MonoBehaviour
{
    public GameObject aimingCirclePrefab;
    public GameObject explosionEffectPrefab; 

    public LayerMask EnemyLayer;
    public LayerMask groundLayer;
    public float attackRadius = 5f;
    public float attackDamage = 50f;

    private GameObject aimingCircle;
    private bool isAiming = false;
    private bool attackExecuted = false;
    private float maxRaycastDistance = 100f;

    // Cooldown
    public float cooldownTime = 5f;
    private float currentCooldownTime = 0f;

    // UI
    public Text cooldownText;
    public Button attackButton;

    // Nazwa obiektu sfery w prefabie
    public string sphereName = "AttackRangeSphere"; // Upewnij się, że dziecko prefab ma taką nazwę!

    void Update()
    {
        // Cooldown odliczanie
        if (currentCooldownTime > 0f)
        {
            currentCooldownTime -= Time.deltaTime;
            cooldownText.text = Mathf.Ceil(currentCooldownTime).ToString();
            attackButton.interactable = false;
        }
        else
        {
            attackButton.interactable = true;
            cooldownText.text = "";
        }

        // Obsługa celowania i ataku
        if (isAiming)
        {
            FollowMouse();

            if (Input.GetMouseButtonDown(0) && currentCooldownTime <= 0f && !attackExecuted)
            {
                ExecuteAttack();
                attackExecuted = true;
                currentCooldownTime = cooldownTime;
            }
        }
    }

    public void StartAiming()
    {
        if (!isAiming && currentCooldownTime <= 0f)
        {
            isAiming = true;
            attackExecuted = false;
            aimingCircle = Instantiate(aimingCirclePrefab);
            aimingCircle.SetActive(true);
        }
    }

    void FollowMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRaycastDistance, groundLayer))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y += 0.5f; // Podniesienie kółka nad ziemię
            aimingCircle.transform.position = targetPosition;
        }
    }

    void ExecuteAttack()
    {
 
        if (explosionEffectPrefab != null)
        {
            Vector3 effectPosition = aimingCircle.transform.position;
            effectPosition.y += 0.5f; 
            Instantiate(explosionEffectPrefab, effectPosition, Quaternion.identity);
        }

        Collider[] enemiesInRange = Physics.OverlapSphere(aimingCircle.transform.position, attackRadius, EnemyLayer);

        foreach (Collider enemy in enemiesInRange)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }

        StopAiming();
    }

    void StopAiming()
    {
        isAiming = false;
        if (aimingCircle != null)
        {
            Destroy(aimingCircle);
        }
    }
}
