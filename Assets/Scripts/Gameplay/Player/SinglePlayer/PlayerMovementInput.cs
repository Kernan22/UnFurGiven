using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody rb;
    private Vector2 moveInput;

    public Transform cameraTransform; // Reference to the camera for movement direction

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0; // Ignore vertical tilt of the camera
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 movement = camForward * moveInput.y + camRight * moveInput.x;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement); // Rotate player to face movement direction
        }
    }
}