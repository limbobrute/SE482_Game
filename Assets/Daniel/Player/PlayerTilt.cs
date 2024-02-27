using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTilt : MonoBehaviour
{
    public Transform pivot; // The pivot joint
    public float tiltAmount = 10f; // You can adjust this value to get the desired tilt level
    public float tiltSpeed = 2f; // You can adjust this value to get the desired tilt speed
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 tilt = new Vector3(-_rb.velocity.z, 0.0f, _rb.velocity.x) * tiltAmount;
        Quaternion targetRotation = Quaternion.Euler(tilt);
        pivot.localRotation = Quaternion.Slerp(pivot.localRotation, targetRotation, Time.deltaTime * tiltSpeed);
    }
}
