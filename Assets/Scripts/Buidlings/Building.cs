using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : BaseBuilding
{
    public BuildingLevel level;
    public BuildingType buildingType;

    public int[] Modifier = new int[5];
}
