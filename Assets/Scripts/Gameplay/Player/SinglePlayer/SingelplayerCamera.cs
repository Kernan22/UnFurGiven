using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class SingleplayerCamera : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera; // Assign your Cinemachine FreeLook camera in the Inspector

    private void Start()
    {
        if (freeLookCamera != null)
        {
            // Configure FreeLook to use the input provider
            var inputProvider = freeLookCamera.GetComponent<CinemachineInputProvider>();
            if (inputProvider == null)
            {
                inputProvider = freeLookCamera.gameObject.AddComponent<CinemachineInputProvider>();
            }
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Ensure the FreeLook camera uses mouse or gamepad input correctly
        if (freeLookCamera != null)
        {
            Vector2 lookInput = context.ReadValue<Vector2>();
            freeLookCamera.m_XAxis.Value += lookInput.x; // Horizontal rotation
            freeLookCamera.m_YAxis.Value += lookInput.y; // Vertical rotation
        }
    }
}