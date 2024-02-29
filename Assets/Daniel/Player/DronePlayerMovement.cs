using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronePlayerMovement : PlayerMovement
{
    public float climbSpeed = 2.0f; // You can adjust this value to get the desired climb speed
    public float minHeight = 0.0f; // Minimum height
    public float maxHeight = 10.0f; // Maximum height

    void FixedUpdate()
    {
        base.Movement();
         // Check for Q and E key presses for downward and upward movement
        if (Input.GetKey(KeyCode.Q) && transform.position.y > minHeight)
        {
            rb.velocity = new Vector3(rb.velocity.x, -climbSpeed, rb.velocity.z);
        }
        else if (Input.GetKey(KeyCode.E) && transform.position.y < maxHeight)
        {
            rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
        }
    }
}
