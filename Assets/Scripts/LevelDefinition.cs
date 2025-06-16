using UnityEngine;
using System.Collections.Generic;

// This attribute allows creating assets of this type directly in the Unity editor
// (Right-click in Project window -> Create -> Game Level)
[CreateAssetMenu(fileName = "NewLevel", menuName = "Game/Level Definition")]
public class LevelDefinition : ScriptableObject
{
    [Header("Level Settings")]
    public string levelName; // e.g., "Goblin Forest"
    
    public List<Wave> waves; 
}

[System.Serializable]
public class Wave
{
    [Header("Wave Settings")]
    public string waveName; 
    public float delayBeforeWave; 
	public float delayBeforeNextWave;
    public List<EnemyGroup> enemyGroups;
}

[System.Serializable]
public class EnemyGroup
{
    [Header("Enemy Group")]
    public GameObject enemyPrefab; 
    public int count; 
    public float spawnInterval; 
}