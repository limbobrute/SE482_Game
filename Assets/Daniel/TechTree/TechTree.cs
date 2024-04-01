using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TechTree : MonoBehaviour
{
    public TechNode[] nodes;
    int index = 0;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // Wait for the end of the frame to ensure all TechNode objects have been initialized
        yield return new WaitForEndOfFrame();

        // Now activate the TechTree
        ActivateNode(index);
    }

    void UnlockNextNodes(int nodeID)
    {
        TechNode currNode = nodes[nodeID]; 
        for (int i = 0; i < currNode.nextNodes.Length; i++)
        {
            TechNode nextNode = currNode.nextNodes[i];
            if(!nextNode.nodeData.Researched)
            {
                nextNode.NodeUnlock();
            }
        }
    }

    public void ActivateNode(int nodeID)
    {
        //Debug.Log("Node: " + nodeID);
        nodes[nodeID].ActivateNode.Invoke();    
        UnlockNextNodes(nodeID);
    }

    public void ResetNodes()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].ResetNode();
        }
        ActivateNode(index);
    }
}
