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
    public float powerupJumpMultiplier = 1.5f; // Additional jump multiplier for power-up
    public float powerupSpeedMultiplier = 2f; // Additional speed multiplier for power-up
    public float powerupDuration = 10f; // Duration of the power-up effect
    public AudioClip powerupSound;
    [Range(0f, 1f)] public float powerupSoundVolume = 0.7f;

    // Collision sound settings
    public AudioClip collisionSound;
    [Range(0f, 1f)] public float collisionSoundVolume = 0.7f;

    public AudioClip treeCollisionSound;
    [Range(0f, 1f)] public float treeCollisionSoundVolume = 0.7f;

    public AudioClip rockCollisionSound;
    [Range(0f, 1f)] public float rockCollisionSoundVolume = 0.7f;

    public AudioClip groundCollisionSound;
    [Range(0f, 1f)] public float groundCollisionSoundVolume = 0.7f;

    public AudioClip waterSplashSound;
    [Range(0f, 1f)] public float waterSplashSoundVolume = 0.7f;

    public GameObject treeEffectPrefab;
    public GameObject rockEffectPrefab;
    public GameObject groundEffectPrefab;
    public GameObject waterSplashEffectPrefab;

    private AudioSource audioSource;
    public Vector3 originalScale;
    private float originalMass;
    private float originalJumpForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = baseSpeed;
        originalJumpForce = jumpForce;

        originalScale = transform.localScale;
        originalMass = rb.mass;

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

        Vector3 movement = (camForward * movementY + camRight * movementX).normalized;

        rb.AddForce(movement * currentSpeed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            PlayCollisionSound();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            PlaySound(groundCollisionSound, groundCollisionSoundVolume);
            InstantiateEffect(groundEffectPrefab, collision.contacts[0].point);
        }

        if (collision.gameObject.CompareTag("Tree"))
        {
            PlaySound(treeCollisionSound, treeCollisionSoundVolume);
            InstantiateEffect(treeEffectPrefab, collision.contacts[0].point);
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            PlaySound(rockCollisionSound, rockCollisionSoundVolume);
            InstantiateEffect(rockEffectPrefab, collision.contacts[0].point);
        }
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

            PlaySound(waterSplashSound, waterSplashSoundVolume);
            InstantiateEffect(waterSplashEffectPrefab, transform.position);
        }

        if (other.CompareTag("Powerup"))
        {
            ApplyPowerup();
            Destroy(other.gameObject);
            PlaySound(powerupSound, powerupSoundVolume);
        }
    }

    private void ApplyPowerup()
    {
        // Store original stats
        transform.localScale = originalScale * powerupScaleMultiplier;
        rb.mass = originalMass * powerupMassMultiplier;
        currentSpeed = baseSpeed * powerupSpeedMultiplier;
        jumpForce = originalJumpForce * powerupJumpMultiplier;

        // Start timer to remove power-up effects
        StartCoroutine(RemovePowerupAfterDelay());
    }

    private IEnumerator RemovePowerupAfterDelay()
    {
        yield return new WaitForSeconds(powerupDuration);

        // Reset to original stats
        transform.localScale = originalScale;
        rb.mass = originalMass;
        currentSpeed = baseSpeed;
        jumpForce = originalJumpForce;
    }

    private void PlayCollisionSound()
    {
        PlaySound(collisionSound, collisionSoundVolume);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }

    private void InstantiateEffect(GameObject effectPrefab, Vector3 position)
    {
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, position, Quaternion.identity);
        }
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
