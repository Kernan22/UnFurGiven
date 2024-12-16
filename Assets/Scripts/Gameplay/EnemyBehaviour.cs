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
        player = GameObject.FindGameObjectWithTag("Player1").transform;
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