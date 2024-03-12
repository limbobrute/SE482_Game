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
//    public Button nodeButton;
//    public Color lockedColor;
//    public Color activatedColor;
//    ColorBlock cb;
    //public Color canActivateColor;
    public int nodeID = 0;
    public int numPreviousNode = 0;
    public bool canInteract = false;
    public bool isActivated = false;
    public bool canActivate = false;

    public UnityEvent ActivateNode;
 
    // Start is called before the first frame update
    void Awake()
    {
        nodeButton = transform.GetComponent<TechNodeModifyButton>();
        ResetNode();
        //cb = nodeButton.colors;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetNode()
    {
        nodeButton.LockedColor();
        NodeLock();
        canInteract = false;
        isActivated = false;
        canActivate = false;
    }
/*    public void ResetLockedColor()
    {
        cb.disabledColor = lockedColor;
        nodeButton.colors = cb;
        //Debug.Log("color reset: " + nodeID);
    }
*/

    public void NodeUnlock()
    {
        //nodeButton.interactable = true;
        nodeButton.ButtonInteractable(true);
        canInteract = true;
    }

    public void NodeLock()
    {
        //nodeButton.interactable = false;
        nodeButton.ButtonInteractable(false);
        canInteract = true;
    }

    public void Activate()
    {
        nodeButton.ActivatedColor();
        NodeLock();
        //        cb.disabledColor = activatedColor;
        //        nodeButton.colors = cb;
        //ActivateNode.Invoke();
        isActivated = true;
        canActivate = false;
        canInteract = false;
    }

        public void TestDisplayEvent()
    {
        Debug.Log("TEST SUCCESSFUL FOR NODE " + nodeID);
    }

    /*    public void AddPreviousNodeCount()
        {
            numPreviousNode++;
        }
    */
}
