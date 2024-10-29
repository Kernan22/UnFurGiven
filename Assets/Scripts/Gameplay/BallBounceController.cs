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
        // Check if the collision is with the other ball
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate the relative velocity (speed of collision)
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