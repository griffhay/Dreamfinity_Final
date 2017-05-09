using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LucableObject : MonoBehaviour {
    Renderer rendererRef;

    private void Awake()
    {
        if (GetComponent<MeshRenderer>()) 
        {
            rendererRef = GetComponent<MeshRenderer>();
        }
        else if(GetComponent<SkinnedMeshRenderer>())
        {
            rendererRef = GetComponent<SkinnedMeshRenderer>();
        }
        
        if(!rendererRef.material.HasProperty("_EmissionColor"))
        {
            rendererRef.material.EnableKeyword("_EmissionColor");

        }
        rendererRef.material.EnableKeyword("_Color");
    }

    private void Start()
    {
    }
       
    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        float emission =  +Mathf.PingPong(Time.time,1f);
        Color baseColor = mat.GetColor("_Color");
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission/2);

        mat.SetColor("_EmissionColor", finalColor);        
    }
}
