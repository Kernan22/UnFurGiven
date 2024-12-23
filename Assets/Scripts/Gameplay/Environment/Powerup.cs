using UnityEngine;

public class Powerup : MonoBehaviour
{
    public System.Action OnPickedUp;
    
    // If the player in Singleplayer has collided with the powerup, it despawns
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            OnPickedUp?.Invoke();
            Destroy(gameObject); 
        }
    }
}