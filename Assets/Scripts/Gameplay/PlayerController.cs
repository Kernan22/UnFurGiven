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

    // Mouse sensitivity
    public float mouseSensitivity = 100f;
    private float rotationX = 0f;

    // Power-up settings
    public float powerupScaleMultiplier = 1.5f;
    public float powerupMassMultiplier = 2f;
    public float powerupDuration = 5f;
    public AudioClip powerupSound;
    [Range(0f, 1f)] public float powerupSoundVolume = 0.7f;

    // Collision sound settings
    public AudioClip collisionSound;
    [Range(0f, 1f)] public float collisionSoundVolume = 0.7f;

    private AudioSource audioSource;
    private Vector3 originalScale;
    private float originalMass;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = baseSpeed;

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

        // Lock the cursor for mouse look
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        Debug.Log($"Movement Input: {movementVector}");
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void OnLook(InputValue lookValue)
    {
        Vector2 mouseDelta = lookValue.Get<Vector2>();
        Debug.Log($"Look Input: {mouseDelta}");
        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }


    public void OnJump(InputValue jumpValue)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void Update()
    {
        // Check for left mouse click to jump
        if (Mouse.current.leftButton.isPressed && isGrounded)
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
            PlaySound(collisionSound, collisionSoundVolume);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            ApplyPowerup();
            Destroy(other.gameObject);
            PlaySound(powerupSound, powerupSoundVolume);
        }
    }

    private void ApplyPowerup()
    {
        transform.localScale = originalScale * powerupScaleMultiplier;
        rb.mass = originalMass * powerupMassMultiplier;
        currentSpeed = baseSpeed;
    }

    private void RemovePowerup()
    {
        transform.localScale = originalScale;
        rb.mass = originalMass;
        currentSpeed = baseSpeed;
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
