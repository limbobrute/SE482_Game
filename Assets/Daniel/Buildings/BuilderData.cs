using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuilderData", menuName = "ScriptableObjects/BuilderData", order = 1)]
public class BuilderData : ScriptableObject
{
    [Header("General Info")]
    public string builderName; // Name of the builder (e.g., "Colonist," "AI")

    [Header("Builder Properties")]
    public bool inUse; // Is the builder currently assigned?
    public int manpower; // Manpower value (e.g., 1 for Colonist, 3 for AI)
}
