using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class TechNode : MonoBehaviour
{
    public TechTreeNode nodeData;
    public TechNode[] nextNodes;
    TechNodeModifyButton nodeButton;
    UIHoverData hoverData;

    public bool canInteract = false;
    public bool isActivated = false;
    public bool canActivate = false;

    public UnityEvent ActivateNode;
 
    // Start is called before the first frame update
    void Start()
    {
        nodeButton = transform.GetComponent<TechNodeModifyButton>();
    }

    void Start()
    {
        hoverData = new UIHoverData();
        SetHoverData();
        ResetNode();
    }

    public void ResetNode()
    {
        nodeButton.LockedColor();
        NodeLock();
        canInteract = false;
        nodeData.Researched = false;
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
        nodeData.Researched = true;
        hoverData.needRequirementPanel = false;
        canActivate = false;
        canInteract = false;
    }

    void SetHoverData()
    {
        hoverData.objectName = nodeData.NodeLabel;
        hoverData.isBuilding = false;
        //hoverData.level = nodeData.buildingLevel;
        hoverData.description = nodeData.Description;
        hoverData.needRequirementPanel = true;
        hoverData.woodCost = nodeData.cost.Wood;
        hoverData.crystalCost = nodeData.cost.Crystal;
        hoverData.metalCost = nodeData.cost.Metal;
        hoverData.synthiaCost = nodeData.cost.Synthia;
        //hoverData.manpowerCost = nodeData.builderCost;
        //hoverData.constructionTime = nodeData.constructionTime;
    }

    public UIHoverData GetHoverData()
    {
        return hoverData;
    }

    /*    public void TestDisplayEvent()
        {
            Debug.Log("TEST SUCCESSFUL FOR NODE " + nodeScriptableObject.NodeID);
        }
    */
}
