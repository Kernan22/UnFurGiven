using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;     // Movement speed of the enemy
    private Transform player;        // Reference to the player
    private Rigidbody rb;            // Rigidbody component

    public System.Action OnEnemyDestroyed; // Delegate to notify spawner
    private string lastTouchedBy;          // Tracks the tag of the last collider

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
        // Track the last object that touched the enemy
        if (collision.gameObject.CompareTag("Player1"))
        {
            lastTouchedBy = "Player1";
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            lastTouchedBy = "Enemy";
        }
        else
        {
            lastTouchedBy = "Other";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy if the enemy falls into the water
        if (other.CompareTag("Water"))
        {
            if (lastTouchedBy == "Player1")
            {
                scoreManager.AddScore(1); // Increment score only if Player1 was the last to touch it
            }
            OnEnemyDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}