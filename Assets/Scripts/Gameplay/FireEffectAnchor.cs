using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectAnchor : MonoBehaviour
{
    void Update()
    {
        // Keep the fire effect pointing upward
        transform.rotation = Quaternion.identity;
    }
}
