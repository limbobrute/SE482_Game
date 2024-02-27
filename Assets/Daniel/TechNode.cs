using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechNode : MonoBehaviour
{
    public TechNode[] nextNodes;
    public Button nodeButton;
    public Color lockedColor;
    public Color activatedColor;
    ColorBlock cb;
    //public Color canActivateColor;
    public int nodeID = 0;
    public int numPreviousNode = 0;
    public bool canInteract = false;
    public bool isActivated = false;
    public bool canActivate = false;


    // Start is called before the first frame update
    void Awake()
    {
        ResetNode();
        cb = nodeButton.colors;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetNode()
    {
        NodeLock();
        canInteract = false;
        isActivated = false;
        canActivate = false;
    }
    public void ResetLockedColor()
    {
        cb.disabledColor = lockedColor;
        nodeButton.colors = cb;
        Debug.Log("color reset: " + nodeID);
    }

    public void NodeUnlock()
    {
        nodeButton.interactable = true;
        canInteract = true;
    }

    public void NodeLock()
    {
        nodeButton.interactable = false;
        canInteract = true;
    }

    public void Activate()
    {
        NodeLock();
        cb.disabledColor = activatedColor;
        nodeButton.colors = cb;
        isActivated = true;
        canActivate = false;
        canInteract = false;
    }

    /*    public void AddPreviousNodeCount()
        {
            numPreviousNode++;
        }
    */
}
