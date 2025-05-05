using UnityEngine;
using UnityEngine.UI;

public class AreaAttackController : MonoBehaviour
{
    public GameObject aimingCirclePrefab;
    public LayerMask enemyLayer;
    public LayerMask groundLayer;
    public float attackRadius = 5f;
    public float attackDamage = 50f;

    private GameObject aimingCircle;
    private bool isAiming = false;
    private bool attackExecuted = false;
    private float maxRaycastDistance = 100f;

    // Czas cooldownu
    public float cooldownTime = 5f; // Czas cooldownu w sekundach
    private float currentCooldownTime = 0f; // Aktualny czas cooldownu

    // UI Elements
    public Text cooldownText; // Tekst do wyœwietlania licznika
    public Button attackButton; // Przycisk ataku

    void Update()
    {
        // Sprawdzamy, czy cooldown jeszcze nie min¹³
        if (currentCooldownTime > 0f)
        {
            currentCooldownTime -= Time.deltaTime; // Zmniejszamy czas cooldownu
            cooldownText.text = Mathf.Ceil(currentCooldownTime).ToString(); // Wyœwietlamy czas w sekundach

            // Blokujemy przycisk, gdy cooldown trwa
            attackButton.interactable = false;
        }
        else
        {
            // Gdy cooldown minie, przycisk jest aktywny
            attackButton.interactable = true;
            cooldownText.text = ""; // Czyœcimy tekst, gdy cooldown siê skoñczy
        }

        // Obs³uga celowania i ataku
        if (isAiming)
        {
            FollowMouse();

            if (Input.GetMouseButtonDown(0) && currentCooldownTime <= 0f && !attackExecuted)
            {
                ExecuteAttack();
                attackExecuted = true;
                currentCooldownTime = cooldownTime; // Resetujemy cooldown po ataku
            }
        }
    }

    public void StartAiming()
    {
        if (!isAiming)
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
            targetPosition.y += 0.5f; // Podnosimy kó³ko nad ziemiê
            aimingCircle.transform.position = targetPosition;
        }
    }

    void ExecuteAttack()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(aimingCircle.transform.position, attackRadius, enemyLayer);

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
