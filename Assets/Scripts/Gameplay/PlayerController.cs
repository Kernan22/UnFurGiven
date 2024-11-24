using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed = 10f;
    private float currentSpeed;
    private bool isSlowed = false;

    public float jumpForce = 5f;
    private bool isGrounded = false;

    public Transform cameraTransform;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Power-up settings
    public float powerupScaleMultiplier = 1.5f;
    public float powerupMassMultiplier = 2f;
    public float powerupDuration = 5f;
    public AudioClip powerupSound;
    [Range(0f, 1f)] public float powerupSoundVolume = 0.7f;

    // Collision sound settings
    public AudioClip collisionSound;
    [Range(0f, 1f)] public float collisionSoundVolume = 0.7f;

    public AudioClip treeCollisionSound;
    [Range(0f, 1f)] public float treeCollisionSoundVolume = 0.7f;

    public AudioClip rockCollisionSound;
    [Range(0f, 1f)] public float rockCollisionSoundVolume = 0.7f;

    public AudioClip groundCollisionSound; // Sound effect for hitting the ground
    [Range(0f, 1f)] public float groundCollisionSoundVolume = 0.7f;

    public AudioClip waterSplashSound; // Sound effect for landing in water
    [Range(0f, 1f)] public float waterSplashSoundVolume = 0.7f;

    public GameObject treeEffectPrefab; // Prefab for tree collision effect
    public GameObject rockEffectPrefab; // Prefab for rock collision effect
    public GameObject groundEffectPrefab; // Prefab for ground collision effect
    public GameObject waterSplashEffectPrefab; // Prefab for water landing effect

    private AudioSource audioSource;
    private Vector3 originalScale;
    private float originalMass;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = baseSpeed;

        // Save the original scale and mass
        originalScale = transform.localScale;
        originalMass = rb.mass;

        // Add an AudioSource if not already present
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.spatialBlend = 1f;

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
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
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

        // Maintain consistent movement control regardless of mass
        Vector3 movement = (camForward * movementY + camRight * movementX).normalized;

        // Scale force dynamically based on mass to keep the same responsiveness
        float forceMultiplier = rb.mass / originalMass;
        rb.AddForce(movement * currentSpeed * forceMultiplier, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
            if (gameOverManager != null)
            {
                gameOverManager.PlayerFallsInWater(gameObject.tag);
            }

            // Play water splash sound
            if (waterSplashSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(waterSplashSound, waterSplashSoundVolume);
            }

            // Instantiate water splash effect
            if (waterSplashEffectPrefab != null)
            {
                Instantiate(waterSplashEffectPrefab, transform.position, Quaternion.identity);
            }
        }

        if (other.CompareTag("Powerup"))
        {
            ApplyPowerup();
            Destroy(other.gameObject);

            if (powerupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(powerupSound, powerupSoundVolume);
            }

            Invoke(nameof(RemovePowerup), powerupDuration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle ground collisions
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (groundCollisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(groundCollisionSound, groundCollisionSoundVolume);
            }

            if (groundEffectPrefab != null)
            {
                Instantiate(groundEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            }
        }

        // Handle tree collisions
        if (collision.gameObject.CompareTag("Tree"))
        {
            if (treeCollisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(treeCollisionSound, treeCollisionSoundVolume);
            }

            if (treeEffectPrefab != null)
            {
                Instantiate(treeEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            }
        }

        // Handle rock collisions
        if (collision.gameObject.CompareTag("Rock"))
        {
            if (rockCollisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(rockCollisionSound, rockCollisionSoundVolume);
            }

            if (rockEffectPrefab != null)
            {
                Instantiate(rockEffectPrefab, collision.contacts[0].point, Quaternion.identity);
            }
        }
    }

    private void ApplyPowerup()
    {
        // Increase size and mass
        transform.localScale = originalScale * powerupScaleMultiplier;
        rb.mass = originalMass * powerupMassMultiplier;

        // Keep the speed consistent regardless of mass
        currentSpeed = baseSpeed;
    }

    private void RemovePowerup()
    {
        transform.localScale = originalScale;
        rb.mass = originalMass;
        currentSpeed = baseSpeed;
    }

    public void ApplySpeedModifier(float multiplier, float duration)
    {
        if (!isSlowed)
        {
            StartCoroutine(SpeedModifierCoroutine(multiplier, duration));
        }
    }

    private IEnumerator SpeedModifierCoroutine(float multiplier, float duration)
    {
        isSlowed = true;
        currentSpeed = baseSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        currentSpeed = baseSpeed;
        isSlowed = false;
    }
}
