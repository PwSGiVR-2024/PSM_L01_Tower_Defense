using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public LevelDefinition levelDefinition;  // przypisz w inspektorze
    public Text waveText;                    // UI do pokazywania fali, przypisz w inspektorze
    public GameObject victoryPanel;          // panel zwyci�stwa, przypisz w inspektorze

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

            // Tutaj wywo�aj swoj� metod� spawnu przeciwnik�w dla currentWave
            // StartCoroutine(SpawnEnemies(currentWave));

            // Zacznij delay do nast�pnej fali (nie czekaj na wyczyszczenie przeciwnik�w)
            if (currentWave.delayBeforeNextWave > 0)
                yield return new WaitForSeconds(currentWave.delayBeforeNextWave);
        }

        // Po ostatniej fali nie ma nast�pnej - wy�wietl komunikat
        waveText.text = "All waves completed!";

        // Tu ewentualnie mo�esz sprawdzi� czy wszystkich przeciwnik�w pokonano:
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
