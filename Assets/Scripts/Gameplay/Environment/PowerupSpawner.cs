using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab; // Reference to the power-up prefab
    public GameObject spawnPlane; // Reference to the power-up spawn plane
    public AudioClip spawnSound; // Sound effect for power-up spawn
    private AudioSource audioSource;

    private GameObject currentPowerup; // Tracks the currently active power-up
    private bool isSpawning = false; // Prevents overlapping spawns

    private void Start()
    {
        // Power up Audio
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource
        audioSource.volume = 0.7f; 
        audioSource.playOnAwake = false; 

        StartSpawning(); // Start spawning at the beginning
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnPowerupRoutine());
        }
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        isSpawning = true;

        // Wait for 10 seconds before spawning
        yield return new WaitForSeconds(10f);

        if (currentPowerup == null)
        {
            SpawnPowerup();
        }

        isSpawning = false; // Allow spawning for the next round
    }

    private void SpawnPowerup()
    {
        Collider planeCollider = spawnPlane.GetComponent<Collider>();
        Vector3 spawnPosition = GetRandomPointInBounds(planeCollider.bounds);

        Debug.Log("Spawning power-up at position: " + spawnPosition);

        // Spawn the power-up and track it
        currentPowerup = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);

        // Play the spawn sound
        if (audioSource != null && spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or spawnSound is missing!");
        }
    }

    public void ResetSpawner()
    {
        // Destroy the current power-up if it exists
        if (currentPowerup != null)
        {
            Destroy(currentPowerup);
        }

        // Restart spawning logic for the next round
        StartSpawning();
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.center.y; 
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}
