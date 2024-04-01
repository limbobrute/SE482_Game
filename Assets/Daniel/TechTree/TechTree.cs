using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TechTree : MonoBehaviour
{
    public TechNode[] nodes;
    int nodeIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart(0.1f)); 
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
        Debug.Log("Node: " + nodeID);
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
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        ActivateNode(nodeIndex);
    }

}
