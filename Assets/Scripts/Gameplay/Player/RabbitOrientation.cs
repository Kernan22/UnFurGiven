using UnityEngine;


// Aligns the rabbit's orientation with the camera's yaw (horizontal rotation), while preserving its initial pitch and roll. This ensures the rabbit always faces
// the direction the camera is looking, maintaining a natural alignment with the player's view.

public class RabbitOrientation : MonoBehaviour
{
    [Header("Camera Reference")]
    public Transform cameraTransform;  // Reference to the main camera or player camera

    private Quaternion initialRotation; // Stores the initial rotation of the rabbit
    
    // Captures the initial rotation of the rabbit when the game starts.
    void Start()
    {
        // Store the initial rotation of the rabbit for later use
        initialRotation = transform.rotation;
    }
    
    /// Updates the rabbit's orientation every frame, matching the camera's yaw to make the rabbit face the camera direction, preserving its pitch and roll.
    
    void LateUpdate()
    {
        // If the camera transform isn't assigned, exit the function to avoid errors
        if (cameraTransform == null) return;

        // Retrieve the camera's current yaw rotation (horizontal angle)
        float cameraYaw = cameraTransform.eulerAngles.y;

        // Apply the camera's yaw to the rabbit's rotation while keeping the original pitch and roll
        transform.rotation = Quaternion.Euler(
            initialRotation.eulerAngles.x,  // Keep the original pitch (x rotation)
            cameraYaw,                      // Update to match the camera's yaw (y rotation)
            initialRotation.eulerAngles.z   // Keep the original roll (z rotation)
        );
    }
}