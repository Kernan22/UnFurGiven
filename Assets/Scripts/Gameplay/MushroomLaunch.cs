using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomLaunch : MonoBehaviour
{
    public float launchForce = 10f; // Adjust the force as needed

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        }
    }
}

