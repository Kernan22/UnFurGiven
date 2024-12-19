using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomLaunch : MonoBehaviour
{
    public float launchForce = 10f; 
    public AudioClip launchSound; 
    public GameObject mushroomEffectPrefab; 
    private AudioSource audioSource;

    private void Start()
    {
        // Audio source for mushroom collision
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
            // Check if the object is the player and adjust the launch force if scaled
            Transform playerTransform = other.transform;
            PlayerController playerController = other.GetComponent<PlayerController>();

            float adjustedLaunchForce = launchForce;
            if (playerController != null)
            {
                float scaleFactor = playerTransform.localScale.y / playerController.originalScale.y; // Account for power-up scale
                adjustedLaunchForce *= scaleFactor; // Adjust force based on player's scale
            }

            // Apply upward force to the player's Rigidbody
            rb.AddForce(Vector3.up * adjustedLaunchForce, ForceMode.Impulse);

            // Play the sound
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