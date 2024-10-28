using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float speed = 10f; // Movement speed of Player 2
    public float bounceForce = 100f; // Adjust this for desired bounce strength
    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from WASD keys
        float horizontal = 0;
        float vertical = 0;

        if (Input.GetKey(KeyCode.W)) vertical = 1;
        if (Input.GetKey(KeyCode.S)) vertical = -1;
        if (Input.GetKey(KeyCode.A)) horizontal = -1;
        if (Input.GetKey(KeyCode.D)) horizontal = 1;

        // Only apply force if there is input
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * speed;
            rb.AddForce(movement, ForceMode.Acceleration);
        }
        else
        {
            // Gradually reduce velocity when no input is given
            rb.velocity = new Vector3(rb.velocity.x * 0.98f, rb.velocity.y, rb.velocity.z * 0.98f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with another player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the direction of the collision and apply a bounce force
            Vector3 bounceDirection = collision.contacts[0].normal;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}