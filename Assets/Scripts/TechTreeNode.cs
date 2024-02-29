using System.Collections.Generic;
using SerializableDictionary.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "TechTreeNode", menuName = "ScriptableObjects/TechTreeNode", order = 1)]
public class TechTreeNode : ScriptableObject
{
    [SerializeField] SerializableDictionary<string, bool> Prequisites = new SerializableDictionary<string, bool>();
    [SerializeField] SerializableDictionary<string, int> Bonues = new SerializableDictionary<string, int>();
    public bool Reserached = false;
    
}
