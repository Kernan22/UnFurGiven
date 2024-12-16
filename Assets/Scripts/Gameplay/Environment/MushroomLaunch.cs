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
            // Apply upward force to the player's Rigidbody
            rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);

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