using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 5f;  // Movement speed of the enemy

    private Transform player;     // Reference to the player
    private Rigidbody rb;         // Rigidbody component

    [Header("Score & Spawning")]
    public System.Action OnEnemyDestroyed;  
    private bool touchedByPlayer1 = false;  // Tracks if the player has interacted with this enemy

    private ScoreManager scoreManager;  // Reference to the ScoreManager for score updates

 
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Find and set reference to the player
        player = GameObject.FindGameObjectWithTag("Player1")?.transform;
        
        // Locate the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();  
    }
    
    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate direction towards the player and apply force
            Vector3 direction = (player.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed, ForceMode.Force);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        // Mark enemy as touched if Player1 collides with it
        if (collision.gameObject.CompareTag("Player1"))
        {
            touchedByPlayer1 = true;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // If the enemy falls into water, check if the player interacted with it
        if (other.CompareTag("Water"))
        {
            if (touchedByPlayer1)
            {
                // Increase the score by 1 if the player touched the enemy
                scoreManager?.AddScore(1);
            }

            // Notify enemy spawner and destroy the enemy
            OnEnemyDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
