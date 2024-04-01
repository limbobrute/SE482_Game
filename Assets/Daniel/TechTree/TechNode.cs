using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class TechNode : MonoBehaviour
{
    public TechTreeNode nodeScriptableObject;
    public TechNode[] nextNodes;
    private TechNodeModifyButton nodeButton;

    public bool canInteract = false;
    public bool isActivated = false;
    public bool canActivate = false;

    public UnityEvent ActivateNode;
 
    // Start is called before the first frame update
    void Start()
    {
        nodeButton = transform.GetComponent<TechNodeModifyButton>();
        ResetNode();
    }

    public void ResetNode()
    {
        nodeButton.LockedColor();
        NodeLock();
        canInteract = false;
        //isActivated = false;
        nodeScriptableObject.Researched = false;
        canActivate = false;
    }

    public void NodeUnlock()
    {
        nodeButton.ButtonInteractable(true);
        canInteract = true;
    }

    public void NodeLock()
    {
        nodeButton.ButtonInteractable(false);
        canInteract = true;
    }

    public void Activate()
    {
        nodeButton.ActivatedColor();
        NodeLock();
        //isActivated = true;
        nodeScriptableObject.Researched = true;
        canActivate = false;
        canInteract = false;
    }

/*    public void TestDisplayEvent()
    {
        Debug.Log("TEST SUCCESSFUL FOR NODE " + nodeScriptableObject.NodeID);
    }
*/
}
