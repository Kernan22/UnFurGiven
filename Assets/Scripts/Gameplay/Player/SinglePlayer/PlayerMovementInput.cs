using UnityEngine;
using UnityEngine.InputSystem;

/// Handles player movement based on camera orientation using Unity's Input System.
public class PlayerMovementInput : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f; // Player movement speed

    [Header("References")]
    public Transform cameraTransform; // Camera reference for directional movement

    private Rigidbody rb;  // Rigidbody component for movement
    private Vector2 moveInput;  // Stores movement input from controller or keyboard
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; 
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();  // Read 2D movement input (WASD or Controller stick)
    }

    
    //Handles player movement based on camera orientation.
    private void FixedUpdate()
    {
        // Get camera's forward and right vectors (ignore vertical tilt)
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;  // Zero out vertical component to prevent unwanted upward movement
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        // Calculate movement direction based on input and camera orientation
        Vector3 movement = (camForward * moveInput.y + camRight * moveInput.x).normalized;

        // Apply movement to Rigidbody position
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Rotate the player to face the direction of movement
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}
