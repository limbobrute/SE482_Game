using UnityEngine;

public class UIElement : MonoBehaviour
{
    // This method can be used with Unity Events
    public void ToggleElement()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    // This method can be used in scripts to set a specific state
    public void SetElementState(bool state)
    {
        gameObject.SetActive(state);
    }
}
