using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireHazard : MonoBehaviour
{
    public float slowMultiplier = 0.5f; 
    public float slowDuration = 5f; 
    public GameObject fireEffectPrefab; 
    public AudioClip fireSound; 
    [Range(0f, 1f)] public float fireSoundVolume = 1f; 

    private AudioSource audioSource;

    private void Start()
    {
        // Audio source of fire
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Audio configuration
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
                Destroy(fireEffect, slowDuration); // Destroys the fire effect after the duration
            }
        }
    }
}

