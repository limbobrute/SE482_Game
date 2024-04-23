using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseGame : MonoBehaviour
{
    public UnityEvent OnPause;
    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(); 
        }
    }

    void Pause()
    {
        Time.timeScale = 0f; // Pauses the game
        isPaused = true;
        OnPause.Invoke(); // Invokes the UnityEvent
    }

    public void Resume()
    {
        Time.timeScale = 1f; // Resumes the game
        isPaused = false;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Quits the game in the editor
        #else
            Application.Quit(); // Quits the game in a build
        #endif
    }
}
