using System.Collections.Generic;
using SerializableDictionary.Scripts;
using UnityEngine;
using Enums;
using System;

[CreateAssetMenu(fileName = "TechTreeNode", menuName = "ScriptableObjects/TechTreeNode", order = 1)]
public class TechTreeNode : ScriptableObject
{
    [Serializable]public struct Cost
    {
        public int Wood;
        public int Crystal;
        public int Metal;
        public int Synthia;
        public int Workforce;
    }

    public string NodeLabel; // make into tool
    public int NodeID;       // make into tool

    [TextAreaAttribute]
    public string Description;
    //[TextAreaAttribute]
    //public string Prereques;
    [SerializeField]public Cost cost;
    [SerializeField]public int ReqResearchLevel;

    [Tooltip("Dictionary of all prequeisites and if they have been reserached")]
    [SerializeField] public SerializableDictionary<string, bool> Prequisites = new SerializableDictionary<string, bool>();
    [Tooltip("Any and all bonues, with the integer repersenting a precentage")]
    [SerializeField] public SerializableDictionary<string, int> Bonuses = new SerializableDictionary<string, int>();
    public bool Researched = false;
    
}
