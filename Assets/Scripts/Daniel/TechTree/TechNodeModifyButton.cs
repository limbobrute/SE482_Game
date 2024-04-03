using UnityEngine;
using UnityEngine.UI;

public class TechNodeModifyButton : MonoBehaviour
{
    Button nodeButton;
    public Color lockedColor;
    public Color activatedColor;
    ColorBlock cb;

    void Awake()
    {
        nodeButton = transform.GetChild(0).GetComponent<Button>();
    }

    private void Start()
    {
        UpdateButtonColors();
    }

    public void LockedColor()
    {
        SetButtonColors(lockedColor);
    }

    public void ActivatedColor()
    {
        SetButtonColors(activatedColor);
    }

    public void ButtonInteractable(bool setting)
    {
        nodeButton.interactable = setting;
    }

    private void UpdateButtonColors()
    {
        cb = nodeButton.colors;
        cb.disabledColor = lockedColor;
        nodeButton.colors = cb;
    }

    private void SetButtonColors(Color color)
    {
        cb = nodeButton.colors;
        cb.disabledColor = color;
        nodeButton.colors = cb;
    }
}