using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireHazard : MonoBehaviour
{
    public float slowMultiplier = 0.5f; // Speed reduction multiplier
    public float slowDuration = 5f; // Duration of the slowdown
    public GameObject fireEffectPrefab; // Fire effect to attach to the player
    public AudioClip fireSound; // Fire sound to play near the campfire
    [Range(0f, 1f)] public float fireSoundVolume = 1f; // Max volume of the fire sound

    private AudioSource audioSource;

    private void Start()
    {
        // Add an AudioSource component if it doesn't already exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure the AudioSource for 3D sound
        audioSource.clip = fireSound;
        audioSource.loop = true; // Fire sound should loop
        audioSource.spatialBlend = 1f; // Set to 3D sound
        audioSource.maxDistance = 10f; // Set the max distance where the sound can be heard
        audioSource.volume = fireSoundVolume;
        audioSource.Play(); // Start playing the fire sound
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            // Apply the speed modifier
            player.ApplySpeedModifier(slowMultiplier, slowDuration);

            // Attach the fire effect
            if (fireEffectPrefab != null)
            {
                GameObject fireEffect = Instantiate(fireEffectPrefab, player.transform.position, Quaternion.identity);
                fireEffect.transform.SetParent(player.transform);
                Destroy(fireEffect, slowDuration); // Destroy the fire effect after the duration
            }
        }
    }
}

