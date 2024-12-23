using System.Collections;
using UnityEngine;

/// Handles the spawning of power-ups at random locations within a defined spawn plane, ensures only one power-up is active at a time.
public class PowerupSpawner : MonoBehaviour
{
    [Header("Power-Up Settings")]
    public GameObject powerupPrefab;  // Power-up prefab to spawn
    public GameObject spawnPlane;     // Plane where power-ups will spawn
    public AudioClip spawnSound;      // Sound effect for spawning power-up

    private AudioSource audioSource;  // Audio source to play sound
    private GameObject currentPowerup; // Reference to the currently active power-up
    private bool isSpawning = false;   // Flag to prevent overlapping spawns

    private void Start()
    {
        // Setup audio source for power-up spawn sound
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.volume = 0.7f;  // Default volume for spawn sound
        audioSource.playOnAwake = false;

        StartSpawning();  // Start the power-up spawning routine
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

        yield return new WaitForSeconds(10f);  // Wait 10 seconds before spawning

        if (currentPowerup == null)
        {
            SpawnPowerup();
        }

        isSpawning = false;  // Allow future spawns
    }
    
    private void SpawnPowerup()
    {
        // Get the collider of the spawn plane
        Collider planeCollider = spawnPlane.GetComponent<Collider>();
        if (planeCollider == null)
        {
            Debug.LogError("Spawn plane does not have a collider!");
            return;
        }

        // Calculate a random position within the plane's bounds
        Vector3 spawnPosition = GetRandomPointInBounds(planeCollider.bounds);

        Debug.Log("Spawning power-up at position: " + spawnPosition);

        // Instantiate the power-up and track the instance
        currentPowerup = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);

        // Play spawn sound if available
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
        // Remove the existing power-up when picked up
        if (currentPowerup != null)
        {
            Destroy(currentPowerup);
        }

        // Restart the spawn cycle
        StartSpawning();
    }
    
    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.center.y;  // Keep power-up at plane height
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}
