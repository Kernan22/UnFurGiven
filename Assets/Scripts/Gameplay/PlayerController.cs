using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private float movementX;
    private float movementY;

    public float hop;
    public float speed;

    private bool isGrounded;
    public bool hasPowerup;

    public Transform cameraTransform;

    // Power-up settings
    public float powerupScaleMultiplier = 1.5f; // Scale factor for the power-up effect
    public float powerupMassMultiplier = 2f; // Mass multiplier for harder knock-back
    public float powerupDuration = 5f; // Duration of the power-up effect in seconds
    public float bounceForce = 10f; // Additional force applied to the other player on collision when powered up

    private Vector3 originalScale;
    private float originalMass;

    // Speed modifier variables
    public float baseSpeed = 10f; // Default movement speed
    private float currentSpeed; // Speed after modifiers
    private bool isSlowed = false; // Prevent overlapping slowdowns

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // For splitscreen
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Save original scale and mass
        originalScale = transform.localScale;
        originalMass = rb.mass;

        // Initialize current speed
        currentSpeed = baseSpeed;
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnJump(InputValue jumpValue)
    {
        // Make sure the players can only jump if they're on the ground
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * hop, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // Scale the speed based on mass to maintain similar control feel
        float adjustedSpeed = currentSpeed * (rb.mass / originalMass);

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

    // Speed modifier functionality
    public void ApplySpeedModifier(float multiplier, float duration)
    {
        if (!isSlowed) // Avoid overlapping effects
        {
            StartCoroutine(SpeedModifierCoroutine(multiplier, duration));
        }
    }

    private IEnumerator SpeedModifierCoroutine(float multiplier, float duration)
    {
        isSlowed = true; // Prevent reapplication
        currentSpeed = baseSpeed * multiplier; // Adjust speed
        yield return new WaitForSeconds(duration); // Wait for the effect to wear off
        currentSpeed = baseSpeed; // Reset speed
        isSlowed = false; // Allow future slowdowns
    }
}
