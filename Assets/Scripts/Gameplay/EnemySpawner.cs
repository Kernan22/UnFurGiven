using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public Transform[] spawnPoints;   // Array of spawn points
    public float spawnInterval = 5f;  // Time between spawns
    public int maxEnemies = 10;       // Maximum number of enemies allowed

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
        // Random spawn point and enemy prefab
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Instantiate and track the new enemy
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        activeEnemies.Add(newEnemy);

        // Remove enemy from list when destroyed
        EnemyBehavior enemyScript = newEnemy.GetComponent<EnemyBehavior>();
        if (enemyScript != null)
        {
            enemyScript.OnEnemyDestroyed += () => activeEnemies.Remove(newEnemy);
        }
    }
}