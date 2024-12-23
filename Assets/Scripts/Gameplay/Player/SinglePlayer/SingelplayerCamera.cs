using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

/// Handles the camera rotation using both controller and mouse input.

public class SingleplayerCamera : MonoBehaviour
{
    [Header("Camera Setup")]
    public CinemachineFreeLook freeLookCamera;  // Reference to the Cinemachine FreeLook camera

    private void Start()
    {
        // Ensure the Cinemachine FreeLook Camera has an input provider
        if (freeLookCamera != null)
        {
            var inputProvider = freeLookCamera.GetComponent<CinemachineInputProvider>();
            
            // Add an input provider if it doesn't exist
            if (inputProvider == null)
            {
                inputProvider = freeLookCamera.gameObject.AddComponent<CinemachineInputProvider>();
            }
        }
        else
        {
            Debug.LogWarning("Cinemachine FreeLook camera is not assigned.");
        }
    }
    
    // Handles camera movement based on mouse or controller input.
    public void OnLook(InputAction.CallbackContext context)
    {
        // Process camera input only if the FreeLook camera is assigned
        if (freeLookCamera != null)
        {
            Vector2 lookInput = context.ReadValue<Vector2>();

            // Adjust horizontal and vertical rotation based on input
            freeLookCamera.m_XAxis.Value += lookInput.x;
            freeLookCamera.m_YAxis.Value += lookInput.y;
        }
    }
}