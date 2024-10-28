using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private float movementX;
    private float movementY;

    public float hop;
    public float speed;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue jumpValue)
    {
        if (isGrounded) // Only allow jumping if grounded
        {
            rb.AddForce(Vector3.up * hop, ForceMode.Impulse);
            isGrounded = false; // Set to false to prevent multiple jumps
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball is touching the ground
        if (collision.gameObject.CompareTag("Ground")) // Ensure the ground has a "Ground" tag
        {
            isGrounded = true;
        }
    }
}