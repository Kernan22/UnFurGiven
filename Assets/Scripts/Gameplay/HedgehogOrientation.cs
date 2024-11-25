using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgehogOrientation : MonoBehaviour
{
    public Transform cameraTransform; 

    private Quaternion initialRotation; 

    void Start()
    {
        // Stores the initial rotation of the parent
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        
        float cameraYaw = cameraTransform.eulerAngles.y;

       
        transform.rotation = Quaternion.Euler(
            initialRotation.eulerAngles.x,
            cameraYaw,
            initialRotation.eulerAngles.z
        );
    }
}
