using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTree : MonoBehaviour
{
    public TechNode[] nodes;
    public int nodeIndex = 0;
    //public bool testOn = false;

    // Start is called before the first frame update
    void Start()
    {
        //SetPreviousNodeCounts();
        ActivateNode(nodeIndex);
    }

/*    void SetPreviousNodeCounts()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            for (int j = 0; j < nodes[i].nextNodes.Length; j++)
            {
                nodes[nodes[i].nextNodes[j].nodeID].AddPreviousNodeCount();
            }
        }
    }
*/
    void UnlockNextNodes(int nodeID)
    {
        TechNode currNode = nodes[nodeID]; 
        for (int i = 0; i < currNode.nextNodes.Length; i++)
        {
            TechNode nextNode = currNode.nextNodes[i];
            if (!nextNode.isActivated)
            {
                nextNode.NodeUnlock();
            }
        }
    }

    public void ActivateNode(int nodeID)
    {
        nodes[nodeID].Activate();
        UnlockNextNodes(nodeID);
    }

/*    public void TestNodes()
    {
        if (!testOn)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].NodeUnlock();
            }
            testOn = true;
        }
        else
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].ResetNode();
                nodes[i].ResetLockedColor();
            }
            ActivateNode(nodeIndex);
            testOn = false;
        }
    }
*/

    public void ResetNodes()
    {
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].ResetNode();
            nodes[i].ResetLockedColor();
        }
        ActivateNode(nodeIndex);
    }
}
