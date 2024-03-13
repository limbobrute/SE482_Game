using UnityEngine;
using TMPro;

public class FPSTracker : MonoBehaviour
{
    float fps;
    float updateTimer = 0.2f;
    public TextMeshProUGUI fpsCounter;

    // Update is called once per frame
    void Update()
    {
        updateTimer -= Time.deltaTime;
        if(updateTimer <= 0f)
        {
            fps = 1f / Time.unscaledDeltaTime;
            fpsCounter.text = "FPS: " + Mathf.RoundToInt(fps);
            updateTimer = 0.2f;
        }
    }
}
