using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab; // Reference to the power-up prefab
    public GameObject spawnPlane; // Reference to the power-up spawn plane
    public AudioClip spawnSound; // Sound effect for power-up spawn
    private AudioSource audioSource;

    private bool hasSpawned = false; // Tracks whether the power-up has been spawned

    private void Start()
    {
        // Add an AudioSource component if not already attached
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource
        audioSource.volume = 0.7f; // Adjust the volume as needed
        audioSource.playOnAwake = false; // Prevent unintended playback
    }

    public void StartSpawning()
    {
        // Calls the SpawnPowerup method to begin spawning
        SpawnPowerup();
    }

    private void SpawnPowerup()
    {
        if (!hasSpawned)
        {
            Collider planeCollider = spawnPlane.GetComponent<Collider>();
            Vector3 spawnPosition = GetRandomPointInBounds(planeCollider.bounds);

            Debug.Log("Spawning power-up at position: " + spawnPosition);

            Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
            hasSpawned = true;

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
        else
        {
            Debug.Log("Power-up has already been spawned, skipping.");
        }
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = bounds.center.y; // Spawn at the y-position of the plane
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}

