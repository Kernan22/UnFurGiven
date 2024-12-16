using UnityEngine;

public class BallBounceController : MonoBehaviour
{
    public float bounceThreshold = 5f; // Minimum force required to bounce
    public float bounceMultiplier = 2f; // Multiplier to control bounce intensity

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with Player1, Player2, or Enemy
        if (collision.gameObject.CompareTag("Player1") || 
            collision.gameObject.CompareTag("Player2") || 
            collision.gameObject.CompareTag("Enemy"))
        {
            // Calculate the relative velocity
            float collisionForce = collision.relativeVelocity.magnitude;

            // Only bounce if the collision force is above the threshold
            if (collisionForce >= bounceThreshold)
            {
                // Calculate bounce direction and apply force
                Vector3 bounceDirection = collision.contacts[0].normal;
                rb.AddForce(bounceDirection * collisionForce * bounceMultiplier, ForceMode.Impulse);
            }
        }
    }
}