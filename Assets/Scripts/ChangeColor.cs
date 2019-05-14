using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color startColor = Color.red;
    [Range(0f, 1f)] public float alpha = 0.5f;

    Renderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();

        m_Renderer.material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
    }

    public void Change(Color newColor)
    {
        m_Renderer.material.color = new Color(newColor.r, newColor.g, newColor.b, alpha);
    }
}
