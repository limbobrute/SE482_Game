using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPerspective : MonoBehaviour
{
    public Transform[] targets; // The array of target objects
    private int currentTargetIndex = 0; // The index of the current target
    //private Camera cam; // The camera

    void Start()
    {
        //cam = GetComponent<Camera>(); // Get the Camera component
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Check if the left shift key is pressed
        {
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length; // Move to the next target in the array
        }

        if (targets[currentTargetIndex] != null) // Check if the target exists
        {
            //cam.transform.position = targets[currentTargetIndex].position; // Set the camera's position to the target's position
            //cam.transform.rotation = targets[currentTargetIndex].rotation; // Set the camera's rotation to the target's rotation
            transform.position = targets[currentTargetIndex].position; // Set the camera's position to the target's position
            transform.rotation = targets[currentTargetIndex].rotation; // Set the camera's rotation to the target's rotation
        }
    }
}
