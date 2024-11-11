using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireHazard : MonoBehaviour
{
    public float slowMultiplier = 0.5f; // Speed reduction multiplier
    public float slowDuration = 5f; // Duration of the slowdown
    public GameObject fireEffectPrefab; // Fire effect to attach to the player

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
                // Instantiate fire effect at player's position
                GameObject fireEffect = Instantiate(fireEffectPrefab, player.transform.position, Quaternion.identity);

                // Parent it to the player
                fireEffect.transform.SetParent(player.transform);

                // Ensure the fire effect always points upward
                fireEffect.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

                // Destroy the fire effect after the duration
                Destroy(fireEffect, slowDuration);
            }
        }
    }
}
