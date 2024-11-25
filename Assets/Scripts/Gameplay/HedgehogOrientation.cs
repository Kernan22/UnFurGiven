using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogOrientation : MonoBehaviour
{
    public Transform cameraTransform; // Assign your camera here

    private Quaternion initialRotation; // Initial rotation of the parent

    void Start()
    {
        // Store the initial rotation of the parent
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Get the Y-axis rotation difference between the camera and initial rotation
        float cameraYaw = cameraTransform.eulerAngles.y;

        // Apply the Y-axis rotation to the parent while keeping the initial X and Z rotations
        transform.rotation = Quaternion.Euler(
            initialRotation.eulerAngles.x,
            cameraYaw,
            initialRotation.eulerAngles.z
        );
    }
}
