using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float smoothTime = 0.3f; // You can adjust this value to get the desired smoothness level

    private Vector3 velocity = Vector3.zero;
    protected Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 2; // Adding drag for smoother stopping
    }

    void FixedUpdate()
    {
        Movement();
    }

    protected void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 targetVelocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, smoothTime);
    }
}
