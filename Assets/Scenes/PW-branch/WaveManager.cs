using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public LevelDefinition levelDefinition;  // przypisz w inspektorze
    public Text waveText;                    // UI do pokazywania fali, przypisz w inspektorze
    public GameObject victoryPanel;          // panel zwyci�stwa, przypisz w inspektorze

    private int currentWaveIndex = 0;

    void Start()
    {
        victoryPanel.SetActive(false);
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        while (currentWaveIndex < levelDefinition.waves.Count)
        {
            UpdateWaveText();

            Wave currentWave = levelDefinition.waves[currentWaveIndex];

            if (currentWave.delayBeforeWave > 0)
                yield return new WaitForSeconds(currentWave.delayBeforeWave);

            // Tu spawnuj przeciwnik�w (musisz doda� w�asn� implementacj�)
            // np. StartCoroutine(SpawnEnemies(currentWave));

            yield return StartCoroutine(WaitForEnemiesDefeated());

            if (currentWave.delayBeforeNextWave > 0)
                yield return new WaitForSeconds(currentWave.delayBeforeNextWave);

            currentWaveIndex++;
            UpdateWaveText(); // wa�ne, �eby zaktualizowa� po inkrementacji
        }

        ShowVictory();
    }

    void UpdateWaveText()
    {
        if (waveText != null)
        {
            if (currentWaveIndex < levelDefinition.waves.Count)
                waveText.text = $"Wave {currentWaveIndex + 1}/{levelDefinition.waves.Count}";
            else
                waveText.text = "All waves completed!";
        }
    }

    IEnumerator WaitForEnemiesDefeated()
    {
        // Czekaj a� wrogowie znikn�
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return null;
        }
    }

    void ShowVictory()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
    }
}
