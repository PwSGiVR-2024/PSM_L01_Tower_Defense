using UnityEngine;
using UnityEngine.UI;

public class Health_behaviour : MonoBehaviour
{
    public int startingLives = 3;
    private int currentLives;

    public Text livesText; // Pod³¹cz w Inspectorze
    public GameObject gameOverPanel; // Pod³¹cz panel Game Over w Inspectorze

    private bool isGameOver = false;

    private void Start()
    {
        currentLives = startingLives;
        UpdateLivesUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // Ukryj panel na start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy wszed³ do triggera!");

            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                currentLives -= enemy.damageToBase;
            }
            else
            {
                currentLives--; // fallback
            }

            UpdateLivesUI();
            Destroy(other.gameObject);

            if (currentLives <= 0 && !isGameOver)
            {
                GameOver();
            }
        }
    }

    private void UpdateLivesUI()
    {
        livesText.text = "Lives: " + currentLives;
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // zatrzymanie gry
    }
}
