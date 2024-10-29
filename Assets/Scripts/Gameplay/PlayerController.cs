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
    public bool hasPowerup;

    public Transform cameraTransform; // Reference to the camera's transform

    // Power-up settings
    public float powerupScaleMultiplier = 1.5f; // Scale factor for the power-up effect
    public float powerupMassMultiplier = 2f; // Mass multiplier for harder knock-back
    public float powerupDuration = 5f; // Duration of the power-up effect in seconds
    public float bounceForce = 10f; // Additional force applied to the other player on collision when powered up

    private Vector3 originalScale;
    private float originalMass;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Ensure each player has their own camera if doing splitscreen
        }

        // Save original scale and mass
        originalScale = transform.localScale;
        originalMass = rb.mass;
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnJump(InputValue jumpValue)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * hop, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // Scale the speed based on mass to maintain similar control feel
        float adjustedSpeed = speed * (rb.mass / originalMass);

        Vector3 movement = camForward * movementY + camRight * movementX;
        rb.AddForce(movement * adjustedSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Apply bounce force to other player if this player has power-up
        if (hasPowerup && collision.gameObject.CompareTag("Player"))
        {
            Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
            if (otherRb != null)
            {
                // Calculate bounce direction from the collision point normal
                Vector3 bounceDirection = collision.contacts[0].normal;
                otherRb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            ApplyPowerup();
            Invoke(nameof(RemovePowerup), powerupDuration); // Remove power-up after duration
        }
    }

    private void ApplyPowerup()
    {
        // Increase player scale and mass
        transform.localScale = originalScale * powerupScaleMultiplier;
        rb.mass = originalMass * powerupMassMultiplier;
    }

    private void RemovePowerup()
    {
        // Reset player scale and mass to original values
        hasPowerup = false;
        transform.localScale = originalScale;
        rb.mass = originalMass;
    }
}
