using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f; // Movement speed
    public float bounceForce = 100f; // Adjust this for desired bounce strength
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * speed;
            rb.AddForce(movement, ForceMode.Acceleration);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x * 0.98f, rb.velocity.y, rb.velocity.z * 0.98f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with another player
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 bounceDirection = collision.contacts[0].normal;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}



