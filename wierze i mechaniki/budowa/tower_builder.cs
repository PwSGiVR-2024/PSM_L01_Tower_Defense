using System;
using UnityEditor;
using UnityEngine;

public class tower_builder : MonoBehaviour
{
    public int towerHeight = 5;
    public float cubeHeight = 1f;
    private GameObject menu;

    public void BuildTower()
    {
        GameObject cubePrefab = Resources.Load<GameObject>("Cube");
        menu = GameObject.FindGameObjectWithTag("Tower_Menu");

        if (cubePrefab == null)
        {
            Debug.LogError("Nie znaleziono prefab'a 'Cube' w folderze Resources!");
            return;
        }

        for (int i = 0; i < towerHeight; i++)
        {
            Vector3 spawnPosition = new Vector3(0, i * cubeHeight, 0);
            Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
            menu.SetActive(false);
        }
    }


}
