using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TechTree : MonoBehaviour
{
    public TechNode[] nodes;
    public int nodeIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        ActivateNode(nodeIndex);
    }

    void UnlockNextNodes(int nodeID)
    {
        TechNode currNode = nodes[nodeID]; 
        for (int i = 0; i < currNode.nextNodes.Length; i++)
        {
            TechNode nextNode = currNode.nextNodes[i];
            //if (!nextNode.isActivated)
            if(!nextNode.nodeScriptableObject.Researched)
            {
                nextNode.NodeUnlock();
            }
        }
    }

    public void ActivateNode(int nodeID)
    {
        nodes[nodeID].ActivateNode.Invoke();
        UnlockNextNodes(nodeID);
    }

    public void ResetNodes()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].ResetNode();
        }
        ActivateNode(nodeIndex);
    }
}
