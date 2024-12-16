using UnityEngine;

public class RabbitOrientation : MonoBehaviour
{
    public Transform cameraTransform; 

    private Quaternion initialRotation; 

    void Start()
    {
        
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