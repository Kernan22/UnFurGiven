using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;     // Movement speed of the enemy
    private Transform player;        // Reference to the player
    private Rigidbody rb;            // Rigidbody component

    public System.Action OnEnemyDestroyed; // Delegate to notify spawner
    private bool touchedByPlayer1 = false; // Tracks if Player1 has touched this enemy

    private ScoreManager scoreManager;     // Reference to ScoreManager

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player1").transform;
        scoreManager = FindObjectOfType<ScoreManager>(); // Locate the ScoreManager
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
        // Check if Player1 has touched the enemy
        if (collision.gameObject.CompareTag("Player1"))
        {
            touchedByPlayer1 = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy if the enemy falls into the water
        if (other.CompareTag("Water"))
        {
            if (touchedByPlayer1)
            {
                scoreManager.AddScore(1); // Increment score if Player1 has touched this enemy
            }
            OnEnemyDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
