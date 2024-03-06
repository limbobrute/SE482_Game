using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelecter : MonoBehaviour
{
    private Renderer render;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Renderer>())
        { render = GetComponent<Renderer>(); color = render.material.color; }
    }

    private void OnMouseEnter()
    {
        render.material.color = Color.blue;
    }

    private void OnMouseExit()
    {
        render.material.color = color;
    }
}
