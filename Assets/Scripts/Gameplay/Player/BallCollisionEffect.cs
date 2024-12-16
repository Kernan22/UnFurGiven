using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionEffect : MonoBehaviour
{
    public GameObject collisionEffectPrefab; 

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is Player1 or Player2
        if ((gameObject.CompareTag("Player1") && collision.gameObject.CompareTag("Player2")) ||
            (gameObject.CompareTag("Player2") && collision.gameObject.CompareTag("Player1")))
        {
            // Calculate the midpoint between Player1 and Player2
            Vector3 midpoint = (transform.position + collision.transform.position) / 2;

            // Make the collision effect happen where the players collide
            if (collisionEffectPrefab != null)
            {
                Instantiate(collisionEffectPrefab, midpoint, Quaternion.identity);
            }
        }
    }
}
