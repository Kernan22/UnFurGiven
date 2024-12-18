using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    private GameEndManager gameEndManager; // Reference to the GameEndManager script

    void Start()
    {
        // Find and assign the GameEndManager
        gameEndManager = FindObjectOfType<GameEndManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if Player1 fell into the water
        if (other.CompareTag("Player1"))
        {
            // Trigger the game over logic
            gameEndManager.TriggerGameOver();

            // Optional: Destroy the player object or deactivate it
            Destroy(other.gameObject);
        }
    }
}