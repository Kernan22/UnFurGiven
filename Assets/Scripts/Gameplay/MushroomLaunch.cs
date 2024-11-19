using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomLaunch : MonoBehaviour
{
    public float launchForce = 10f; // Adjust the force as needed
    public AudioClip launchSound; // Sound to play on collision
    public GameObject mushroomEffectPrefab; // Animation or particle effect prefab to play on collision
    private AudioSource audioSource;

    private void Start()
    {
        // Add an AudioSource to the mushroom if it doesn't exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply upward force to the player's Rigidbody
            rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);

            // Play the launch sound
            if (audioSource != null && launchSound != null)
            {
                audioSource.PlayOneShot(launchSound);
            }

            // Play the collision effect
            if (mushroomEffectPrefab != null)
            {
                Instantiate(mushroomEffectPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}