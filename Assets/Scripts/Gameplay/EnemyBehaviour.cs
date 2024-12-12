using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    private GameObject player;

    private Rigidbody rb;

    public System.Action OnDestroyed; // Event for notifying when destroyed

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player"); // Ensure the player is tagged as "Player"

        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing from the enemy prefab!");
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * moveSpeed, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Remove the enemy if it falls off the island
        if (other.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}