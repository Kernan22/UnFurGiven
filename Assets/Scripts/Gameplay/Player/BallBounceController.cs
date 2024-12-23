using UnityEngine;

// Controls ball bounce behavior based on collision force and direction. Bounces the ball off Player1, Player2, or Enemy with intensity proportional to collision force.

public class BallBounceController : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceThreshold = 5f;  // Minimum force required to trigger a bounce
    public float bounceMultiplier = 2f; // Multiplier to amplify the bounce intensity

    private Rigidbody rb;  // Rigidbody component for applying physics-based forces
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Detects collisions and applies bounce force if the collision meets the threshold.
   
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with Player1, Player2, or Enemy
        if (collision.gameObject.CompareTag("Player1") || 
            collision.gameObject.CompareTag("Player2") || 
            collision.gameObject.CompareTag("Enemy"))
        {
            // Calculate the force of the collision based on relative velocity
            float collisionForce = collision.relativeVelocity.magnitude;

            // Apply bounce only if the collision force exceeds the threshold
            if (collisionForce >= bounceThreshold)
            {
                // Determine bounce direction (normal of collision contact point)
                Vector3 bounceDirection = collision.contacts[0].normal;

                // Apply impulse force in the direction of bounce, scaled by collision force and multiplier
                rb.AddForce(bounceDirection * collisionForce * bounceMultiplier, ForceMode.Impulse);
            }
        }
    }
}