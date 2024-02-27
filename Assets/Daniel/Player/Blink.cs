using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public float delay = 3.0f; // Delay before the blink starts
    public float blinkDuration = 0.5f; // Duration of the blink
    public float minScale = 0.1f; // Minimum scale during the blink

    private Vector3 originalScale; // Original scale of the object

    void Start()
    {
        originalScale = transform.localScale; // Save the original scale
        StartCoroutine(BlinkCoroutine()); // Start the blink coroutine
    }

    IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay); // Wait for the delay

            // Shrink the z-scale
            for (float t = 0; t < blinkDuration; t += Time.deltaTime)
            {
                float scaleFactor = Mathf.Lerp(1, minScale, t / blinkDuration);
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z * scaleFactor);
                yield return null;
            }

            // Return to original size
            for (float t = 0; t < blinkDuration; t += Time.deltaTime)
            {
                float scaleFactor = Mathf.Lerp(minScale, 1, t / blinkDuration);
                transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z * scaleFactor);
                yield return null;
            }
        }
    }
}
