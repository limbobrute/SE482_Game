using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text Header; // Reference to the Header Text component
    public Text Description; // Reference to the Description Text component
    public Text Requirements; // Reference to the Requirements Text component
    private TechNode parentNode;

    void Awake()
    {
        parentNode = transform.parent.transform.GetComponent<TechNode>();
    }

    public void UpdatePanel()
    {
        // Update the text fields when the mouse pointer enters the button
        //Header.text = "Node " + parentNode.nodeScriptableObject.NodeName;
        Header.text = parentNode.nodeScriptableObject.NodeLabel;

        Description.text = parentNode.nodeScriptableObject.Description;
        if (parentNode.nodeScriptableObject.Researched)
        {
            Requirements.text = "RESEARCHED";
        }
        else
        {
            Requirements.text = parentNode.nodeScriptableObject.Prereques;
        }

        Header.GetComponent<Text>().enabled = true;
        Description.GetComponent<Text>().enabled = true;
        Requirements.GetComponent<Text>().enabled = true;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdatePanel();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Header.GetComponent<Text>().enabled = false;
        Description.GetComponent<Text>().enabled = false;
        Requirements.GetComponent<Text>().enabled = false;

        // Clear the text fields when the mouse pointer exits the button
        Header.text = "";
        Description.text = "";
        Requirements.text = "";
    }
}