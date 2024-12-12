using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Drag your 4 enemy prefabs here
    public Transform[] spawnPoints; // Assign your 5 island transforms here
    public float spawnInterval = 5f; // Time between spawns
    public int maxEnemies = 10; // Maximum number of enemies on the map

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (activeEnemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Randomly choose a spawn point
        Transform chosenSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Randomly choose an enemy prefab
        GameObject chosenPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Spawn the enemy at the chosen spawn point
        GameObject newEnemy = Instantiate(chosenPrefab, chosenSpawnPoint.position, Quaternion.identity);

        // Track the spawned enemy
        activeEnemies.Add(newEnemy);

        // Remove the enemy when destroyed
        EnemyBehavior enemyBehavior = newEnemy.GetComponent<EnemyBehavior>();
        if (enemyBehavior != null)
        {
            enemyBehavior.OnDestroyed += () => activeEnemies.Remove(newEnemy);
        }
    }
}