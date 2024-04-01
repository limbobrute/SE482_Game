using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 10.0f; // Speed of the camera movement

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D, Left/Right Arrow)
        float verticalInput = Input.GetAxis("Vertical"); // Get vertical input (W/S, Up/Down Arrow)

        // Calculate new position
        Vector3 newPosition = new Vector3(-horizontalInput, 0, -verticalInput) * speed * Time.deltaTime;

        // Move the camera smoothly to the new position
        transform.position += newPosition;
    }
}
