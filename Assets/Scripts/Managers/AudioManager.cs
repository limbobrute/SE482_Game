using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips; // Assign your audio files here in the inspector
    public UnityEvent onStart;
    private List<AudioSource> audioSources = new List<AudioSource>();

    void Start()
    {
        for (int i = 0; i < audioClips.Length; i++)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = audioClips[i];
            newAudioSource.loop = true;
            audioSources.Add(newAudioSource);
        }

        onStart?.Invoke();
    }

    public void PlayAudio(int clipNumber)
    {
        if (clipNumber >= 0 && clipNumber < audioSources.Count)
        {
            audioSources[clipNumber].Play();
        }
        else
        {
            Debug.Log("Clip number is out of range");
        }
    }

    public void StopAudio(int clipNumber)
    {
        if (clipNumber >= 0 && clipNumber < audioSources.Count)
        {
            audioSources[clipNumber].Stop();
        }
        else
        {
            Debug.Log("Clip number is out of range");
        }
    }

    public void QuietVolume(int clipNumber)
    {
        if (clipNumber >= 0 && clipNumber < audioSources.Count)
        {
            audioSources[clipNumber].volume = .15f;
        }
        else
        {
            Debug.Log("Clip number is out of range");
        }
    }

}
