using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f; // Movement speed of the ball

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the ball
        rb = GetComponent<Rigidbody>();
    }
    
  void Update()
  {
      // Get input from the arrow keys
      float horizontal = Input.GetAxis("Horizontal");
      float vertical = Input.GetAxis("Vertical");
  
      // Only apply force if there is input
      if (horizontal != 0 || vertical != 0)
      {
          Vector3 movement = new Vector3(horizontal, 0, vertical).normalized * speed;
          rb.AddForce(movement, ForceMode.Acceleration);
      }
      else
      {
          // Slightly reduce velocity when no input is given to avoid stopping too quickly
          rb.velocity = new Vector3(rb.velocity.x * 0.98f, rb.velocity.y, rb.velocity.z * 0.98f);
      }
  }

   }





