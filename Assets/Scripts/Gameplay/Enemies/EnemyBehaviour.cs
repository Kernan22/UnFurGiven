using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;     // Movement speed of the enemy
    private Transform player;        // Reference to the player
    private Rigidbody rb;            // Rigidbody component

    public System.Action OnEnemyDestroyed; // Delegate to notify spawner

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player1")?.transform;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRb = collision.rigidbody;

        // Ensure the colliding object has a Rigidbody
        if (otherRb != null)
        {
            Vector3 relativeVelocity = rb.velocity - otherRb.velocity;

            if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Enemy"))
            {
                // Calculate bounce force based on relative velocity and direction
                Vector3 bounceForce = relativeVelocity.normalized * relativeVelocity.magnitude * 2f; // Adjust multiplier as needed
                rb.AddForce(-bounceForce, ForceMode.Impulse);
                otherRb.AddForce(bounceForce, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy if the enemy falls off the level
        if (other.CompareTag("Water"))
        {
            OnEnemyDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}