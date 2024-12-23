using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Aligns the Hedgehog's orientation with the camera's yaw (horizontal rotation), while preserving its initial pitch and roll. This ensures the rabbit always faces
// the direction the camera is looking, maintaining a natural alignment with the player's view.

public class HedgehogOrientation : MonoBehaviour
{
    [Header("Camera Reference")]
    public Transform cameraTransform;  // Reference to the camera the object will align with

    private Quaternion initialRotation;  // Stores the object's initial rotation

  
    // Captures the initial rotation of the object at the start.
    
    void Start()
    {
        // Store the initial rotation of the object (typically the parent or self)
        initialRotation = transform.rotation;
    }
    
    // Called after Update() to adjust orientation, ensuring smooth tracking of the camera's Y-axis.
    void LateUpdate()
    {
        // Exit if no camera is assigned
        if (cameraTransform == null) return;

        // Get the yaw (rotation around Y-axis) of the camera
        float cameraYaw = cameraTransform.eulerAngles.y;

        // Rotate the object to match the camera's yaw while preserving the initial X and Z rotations
        transform.rotation = Quaternion.Euler(
            initialRotation.eulerAngles.x,  // Maintain initial X rotation
            cameraYaw,                      // Align Y rotation with the camera's Yaw
            initialRotation.eulerAngles.z   // Maintain initial Z rotation
        );
    }
}