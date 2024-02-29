using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float spinSpeed = 100f;

    void FixedUpdate()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}
