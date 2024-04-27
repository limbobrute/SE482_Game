using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class TechNode : MonoBehaviour
{
    [SerializeField] ResourceManager resourceManager;
    [SerializeField] AudioManager audioManager;
    public TechTreeNode nodeData;

    public bool canInteract = false;
    public bool canActivate = false;
    public bool isActivated = false;

    public TechNode[] nextNodes;
    TechNodeModifyButton nodeButton;
    ObjectDataForUI hoverData;

    public UnityEvent ActivateNode;
 
    // Start is called before the first frame update
    void Awake()
    {
        nodeButton = transform.GetComponent<TechNodeModifyButton>();
        //hoverData = new ObjectDataForUI();
        //SetHoverData();
        //ResetNode();
    }

    void Start()
    {
        hoverData = new ObjectDataForUI();
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
        if (canResearch())
        {
            nodeButton.ActivatedColor();
            NodeLock();
            audioManager.PlayAudioOnce(3);
            nodeData.Researched = true;
            hoverData.needRequirementPanel = false;
            isActivated = true;
            canActivate = false;
            canInteract = false;
            DeductResources();
        }
        else
        {
            audioManager.PlayAudioOnce(4);
            //Debug.Log("CANNOT RESEARCH!");
        }
    }

    bool canResearch()
    {
        bool costCheck = resourceManager.CanResearch(nodeData);
        return costCheck;
    }

    void DeductResources()
    {
        resourceManager.DeductResource(nodeData.cost);
    }

    void SetHoverData()
    {
        hoverData.objectName = nodeData.NodeLabel;
        hoverData.isBuilding = false;
        //hoverData.level = nodeData.buildingLevel;
        hoverData.description = nodeData.Description;
        hoverData.needRequirementPanel = true;
        hoverData.woodCost = nodeData.cost.Wood.ToString();
        hoverData.crystalCost = nodeData.cost.Crystal.ToString();
        hoverData.metalCost = nodeData.cost.Metal.ToString();
        hoverData.synthiaCost = nodeData.cost.Synthia.ToString();
        //hoverData.manpowerCost = nodeData.builderCost;
        //hoverData.constructionTime = nodeData.constructionTime;
    }

    public ObjectDataForUI GetHoverData()
    {
        return hoverData;
    }

    /*    public void TestDisplayEvent()
        {
            Debug.Log("TEST SUCCESSFUL FOR NODE " + nodeScriptableObject.NodeID);
        }
    */
}
