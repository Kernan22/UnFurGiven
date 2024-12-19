using UnityEngine;

public class Powerup : MonoBehaviour
{
    public System.Action OnPickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            OnPickedUp?.Invoke();
            Destroy(gameObject);
        }
    }
}