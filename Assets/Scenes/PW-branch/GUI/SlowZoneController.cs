using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlowZoneController : MonoBehaviour
{
    public GameObject aimingCirclePrefab;
    public LayerMask groundLayer;
    public LayerMask EnemyLayer;

    public GameObject slowEffectZonePrefab; // prefab z efektem wizualnym, np. particle system

    public float radius = 5f;
    public float slowAmount = 0.5f;  // spowalnianie do 50% prêdkoœci
    public float slowDuration = 3f;
    public float cooldownTime = 8f;

    public Button slowButton;
    public Text cooldownText;

    private GameObject aimingCircle;
    private bool isAiming = false;
    private bool effectUsed = false;
    private float currentCooldownTime = 0f;

    private float maxRaycastDistance = 100f;

    void Update()
    {
        // Cooldown
        if (currentCooldownTime > 0f)
        {
            currentCooldownTime -= Time.deltaTime;
            cooldownText.text = Mathf.Ceil(currentCooldownTime).ToString();
            slowButton.interactable = false;
        }
        else
        {
            cooldownText.text = "";
            slowButton.interactable = true;
        }

        // Celowanie
        if (isAiming)
        {
            FollowMouse();

            if (Input.GetMouseButtonDown(0) && currentCooldownTime <= 0f && !effectUsed)
            {
                PlaceSlowEffect();
                effectUsed = true;
                currentCooldownTime = cooldownTime;
            }
        }
    }

    public void StartAiming()
    {
        if (!isAiming && currentCooldownTime <= 0f)
        {
            isAiming = true;
            effectUsed = false;

            if (aimingCircle != null) Destroy(aimingCircle);

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
            Vector3 targetPos = hit.point;
            targetPos.y += 0.5f;
            aimingCircle.transform.position = targetPos;
        }
    }

    void PlaceSlowEffect()
    {
        if (aimingCircle == null) return;

        Vector3 spawnPos = aimingCircle.transform.position + new Vector3(0f, 0.5f, 0f);
        GameObject slowEffect = Instantiate(slowEffectZonePrefab, spawnPos, Quaternion.identity);

        // Spowalnianie przeciwników w strefie
        Collider[] enemies = Physics.OverlapSphere(spawnPos, radius, EnemyLayer);

        foreach (Collider enemy in enemies)
        {
            EnemyMovement move = enemy.GetComponent<EnemyMovement>();
            if (move != null)
            {
                move.ApplySlow(slowAmount, slowDuration);
            }
        }

        // Opcjonalnie mo¿esz dodaæ efekt wizualny który po czasie znika
        StartCoroutine(DestroyEffectAfterSeconds(slowEffect, slowDuration));

        StopAiming();
    }

    IEnumerator DestroyEffectAfterSeconds(GameObject effect, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(effect);
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
