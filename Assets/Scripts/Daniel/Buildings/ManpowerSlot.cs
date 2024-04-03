using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManpowerSlot : MonoBehaviour
{
    public bool isOccupied; // Is the slot currently used?
    public float efficiency;
    public BuilderData builder; // Reference to the placed colonist or AI 
}
