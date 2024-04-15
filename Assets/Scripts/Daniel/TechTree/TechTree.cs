using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TechTree : MonoBehaviour
{
    [SerializeField] int researchLevel = 0;
    public TechNode[] nodes;
    List<TechNode> awaitingResearch = new List<TechNode>();
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
            if (!nextNode.nodeData.Researched)
            {
                if (nextNode.nodeData.ReqResearchLevel > researchLevel)
                {
                    awaitingResearch.Add(nextNode);
                }
                else
                {
                    nextNode.NodeUnlock();
                }
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

    public void IncreaseResearchLevel()
    {
        researchLevel++;
        UnlockAwaitingResearch();   // Unlock the nodes that couldn't unlock before due to research level.
    }

    public void UnlockAwaitingResearch()
    {
        if(awaitingResearch.Count > 0)
        {
            List<TechNode> nodesToUnlock = new List<TechNode>(awaitingResearch);
            foreach (TechNode node in nodesToUnlock)
            {
                node.NodeUnlock();
                awaitingResearch.Remove(node);
            }
        }
    }
}
