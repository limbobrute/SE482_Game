using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Zoom Variables")]
    public float speed = 10.0f; // Speed of the camera movement
    public float zoomSpeed = 5.0f; // Speed of the camera zoom
    public float minZoom = 10.0f; // Minimum zoom distance
    public float maxZoom = 50.0f; // Maximum zoom distance

    // Define the movement constraints
    [Header("Movement Contraint Variables")]
    public float minX = -30;
    public float maxX = 60;
    [Tooltip("Z-axis is inverted.")] public float minZ = 30;
    [Tooltip("Z-axis is inverted.")] public float maxZ = -60;

    private bool isRotating = false;
    private Vector3 lastMousePosition;

    // Rotation speed (adjust as needed)
    public float rotationSpeed = 0.2f;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D, Left/Right Arrow)
        float verticalInput = Input.GetAxis("Vertical"); // Get vertical input (W/S, Up/Down Arrow)

        // Calculate new position
        Vector3 newPosition = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;
        // Rotate the new position by the camera's y-axis rotation
        newPosition = Quaternion.Euler(0, transform.eulerAngles.y, 0) * newPosition;

        // Add the new position to the current position
        Vector3 targetPosition = transform.position + newPosition;

        // Constrain the target position within the defined bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, maxZ, minZ);

        // Move the camera smoothly to the new position
        transform.position = targetPosition;

        // Zoom using mouse wheel (adjust FOV)
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        float zoom = Camera.main.fieldOfView - zoomInput * zoomSpeed; // Adjust FOV
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        Camera.main.fieldOfView = zoom;

        // Rotate the camera when right mouse button is held down
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }

        if (isRotating)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            float rotationAmount = mouseDelta.x * rotationSpeed;
            transform.Rotate(Vector3.up, rotationAmount, Space.World);
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
    }
}
