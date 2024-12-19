using UnityEngine;
using TMPro;

public class BossSpawner : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign your score TextMeshPro element in the Inspector
    public GameObject bossPrefab;    // Assign the Boss prefab
    public Transform[] spawnPoints; // Array of possible spawn points for the boss

    private int lastBossSpawnScore = 0; // Tracks the score when the last boss spawned

    void Update()
    {
        int currentScore = 0;

        // Safely parse the score, assuming the text might include a label (e.g., "Score: 15")
        if (int.TryParse(System.Text.RegularExpressions.Regex.Match(scoreText.text, @"\d+").Value, out currentScore))
        {
            // Check if the score is a multiple of 15 and ensure it's not a repeat spawn
            if (currentScore >= 15 && currentScore % 15 == 0 && currentScore != lastBossSpawnScore)
            {
                SpawnBoss();
                lastBossSpawnScore = currentScore; // Update the last spawn score
            }
        }
        else
        {
            Debug.LogWarning("Score text is not in a valid format.");
        }
    }

    private void SpawnBoss()
    {
        // Choose the closest spawn point to the player or any other logic
        Transform spawnPoint = FindClosestSpawnPointToPlayer();
        if (spawnPoint != null)
        {
            Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Boss Spawned!");
        }
    }

    private Transform FindClosestSpawnPointToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player1");
        if (player == null) return null;

        Transform closest = null;
        float closestDistance = float.MaxValue;

        foreach (Transform spawnPoint in spawnPoints)
        {
            float distance = Vector3.Distance(spawnPoint.position, player.transform.position);
            if (distance < closestDistance)
            {
                closest = spawnPoint;
                closestDistance = distance;
            }
        }

        return closest;
    }
}
