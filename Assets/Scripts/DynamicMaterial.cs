using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMaterial : MonoBehaviour
{
    public Material[] materials;
    private Renderer rend;
    private float minHeight = 4f;
    private float maxHeight = 20f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        UpdateMaterialColor();
    }

    void Update()
    {
        // Check if the position has changed
        if (transform.hasChanged)
        {
            // Change the material color based on the new position
            UpdateMaterialColor();
            transform.hasChanged = false;
        }
    }

    void UpdateMaterialColor()
    {
        //// Sets Colors based on Height
        //float currentPositionY = transform.position.y;
        //float normalizedHeight = Mathf.InverseLerp(minHeight, maxHeight, currentPositionY);
        //int materialIndex = Mathf.FloorToInt(normalizedHeight * (materials.Length - 1));
        //materialIndex = Mathf.Clamp(materialIndex, 0, materials.Length - 1);
        //rend.material = materials[materialIndex];

        // Sets colors based on Scale
        float currentScaleZ = transform.localScale.z;
        float normalizedHeight = Mathf.InverseLerp(minHeight, maxHeight, currentScaleZ);
        int materialIndex = Mathf.FloorToInt(normalizedHeight * (materials.Length - 1));
        materialIndex = Mathf.Clamp(materialIndex, 0, materials.Length - 1);
        rend.material = materials[materialIndex];
    }
}
