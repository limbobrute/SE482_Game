using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundController : MonoBehaviour
{
    public AudioClip successfulSound;
    public AudioClip unsuccessfulSound;
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    public void OnButtonClick(bool successful)
    {
        // Play the appropriate sound based on the success parameter
        if (successful)
        {
            // Play the successful sound
            if (successfulSound != null)
            {
                audioSource.clip = successfulSound;
                audioSource.Play();
            }
        }
        else
        {
            // Play the unsuccessful sound
            if (unsuccessfulSound != null)
            {
                audioSource.clip = unsuccessfulSound;
                audioSource.Play();
            }
        }
    }
}