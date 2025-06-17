using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public LevelDefinition levelDefinition;  // przypisz w inspektorze
    public Text waveText;                    // UI do pokazywania fali, przypisz w inspektorze
    public GameObject victoryPanel;          // panel zwyciêstwa, przypisz w inspektorze

    private int currentWaveIndex = -1;

    void Start()
    {
        victoryPanel.SetActive(false);
        StartCoroutine(StartAllWaves());
    }

    IEnumerator StartAllWaves()
    {
        for (int i = 0; i < levelDefinition.waves.Count; i++)
        {
            currentWaveIndex = i;
            UpdateWaveText();

            Wave currentWave = levelDefinition.waves[i];

            if (currentWave.delayBeforeWave > 0)
                yield return new WaitForSeconds(currentWave.delayBeforeWave);

            // Tutaj wywo³aj swoj¹ metodê spawnu przeciwników dla currentWave
            // StartCoroutine(SpawnEnemies(currentWave));

            // Zacznij delay do nastêpnej fali (nie czekaj na wyczyszczenie przeciwników)
            if (currentWave.delayBeforeNextWave > 0)
                yield return new WaitForSeconds(currentWave.delayBeforeNextWave);
        }

        // Po ostatniej fali nie ma nastêpnej - wyœwietl komunikat
        waveText.text = "All waves completed!";

        // Tu ewentualnie mo¿esz sprawdziæ czy wszystkich przeciwników pokonano:
        StartCoroutine(WaitForAllEnemiesDefeatedThenVictory());
    }

    void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = $"Wave {currentWaveIndex + 1}/{levelDefinition.waves.Count}";
        }
    }

    IEnumerator WaitForAllEnemiesDefeatedThenVictory()
    {
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return null;
        }

        ShowVictory();
    }

    void ShowVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }
}
