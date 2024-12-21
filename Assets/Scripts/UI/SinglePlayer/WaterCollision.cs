using UnityEngine;


public class WaterCollision : MonoBehaviour
{
    private GameEndManager gameEndManager;

    private void Start()
    {
        gameEndManager = FindObjectOfType<GameEndManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")) // Check if the player falls into the water
        {
            if (gameEndManager != null)
            {
                gameEndManager.TriggerGameOver();
            }
        }
    }
}
