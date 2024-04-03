using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float speed = 10.0f; // Speed of the camera movement

    // Define the movement constraints
    public float minX = -30;
    public float maxX = 60;
    public float minZ = -60;
    public float maxZ = 30;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D, Left/Right Arrow)
        float verticalInput = Input.GetAxis("Vertical"); // Get vertical input (W/S, Up/Down Arrow)

        // Calculate new position
        Vector3 newPosition = new Vector3(-horizontalInput, 0, -verticalInput) * speed * Time.deltaTime;

        // Add the new position to the current position
        Vector3 targetPosition = transform.position + newPosition;

        // Constrain the target position within the defined bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        // Move the camera smoothly to the new position
        transform.position = targetPosition;
    }
}