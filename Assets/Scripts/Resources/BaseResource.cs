using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseResource : MonoBehaviour
{
    public enum ResourceType { Food, Metal, Oxyogen, Water, Cythia}
    public enum RarityLevel { Common, Uncommon, Rare, Exotic}
    public ResourceType ResourceCatagory;
    public RarityLevel RsourceLevel;
}
