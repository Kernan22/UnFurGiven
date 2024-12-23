using UnityEngine;


// Handles player collisions with water and triggers the game-over sequence.

public class WaterCollision : MonoBehaviour
{
    private GameEndManager gameEndManager; // Reference to the GameEndManager to handle game over events
    
    private void Start()
    {
        gameEndManager = FindObjectOfType<GameEndManager>();
    }
    
    // Detects if a player enters the trigger zone (water).
 
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is Player1
        if (other.CompareTag("Player1")) 
        {
            // Trigger the game over event if GameEndManager is found
            if (gameEndManager != null)
            {
                gameEndManager.TriggerGameOver();
            }
            else
            {
                Debug.LogWarning("GameEndManager not found in the scene.");
            }
        }
    }
}