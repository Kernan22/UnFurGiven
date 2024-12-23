using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles collision effects between Player1, Player2, and other entities such as Enemy and Boss. Spawns a visual effect at the midpoint of the collision.

public class PlayerCollisionEffect : MonoBehaviour
{
    [Header("Collision Effect")]
    public GameObject collisionEffectPrefab; // Prefab for visual effect on collision

    
    // Triggered when this GameObject collides with another.
    
    private void OnCollisionEnter(Collision collision)
    {
        // Check for collisions between Player1, Player2, Enemy and Boss
        if ((gameObject.CompareTag("Player1") && collision.gameObject.CompareTag("Player2")) ||
            (gameObject.CompareTag("Player2") && collision.gameObject.CompareTag("Player1")) ||
            (gameObject.CompareTag("Player1") && collision.gameObject.CompareTag("Enemy")) ||  
            (gameObject.CompareTag("Player2") && collision.gameObject.CompareTag("Enemy")) ||  
            (gameObject.CompareTag("Player1") && collision.gameObject.CompareTag("Boss")) ||   
            (gameObject.CompareTag("Player2") && collision.gameObject.CompareTag("Boss")))     
        {
            // Calculate the midpoint between this object and the collided object
            Vector3 midpoint = (transform.position + collision.transform.position) / 2;

            // Instantiate the collision effect at the midpoint if the prefab is assigned
            if (collisionEffectPrefab != null)
            {
                Instantiate(collisionEffectPrefab, midpoint, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Collision effect prefab not assigned to PlayerCollisionEffect.");
            }
        }
    }
}