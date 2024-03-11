using System.Collections.Generic;
using SerializableDictionary.Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "TechTreeNode", menuName = "ScriptableObjects/TechTreeNode", order = 1)]
public class TechTreeNode : ScriptableObject
{
    [TextAreaAttribute]
    public string Description;
    [TextAreaAttribute]
    public string Prereques;

    [Tooltip("Dictionary of all prequeisites and if they have been reserached")]
    [SerializeField] SerializableDictionary<string, bool> Prequisites = new SerializableDictionary<string, bool>();
    [Tooltip("Any and all bonues, with the integer repersenting a precentage")]
    [SerializeField] SerializableDictionary<string, int> Bonues = new SerializableDictionary<string, int>();
    public bool Reserached = false;
    
}
