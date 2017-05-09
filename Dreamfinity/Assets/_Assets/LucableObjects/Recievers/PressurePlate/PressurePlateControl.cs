using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C_Utilities;

public class PressurePlateControl : MonoBehaviour {

    Bounds boundsRef;
    public bool activated;
    float time;
    Vector3 unActivePos, activePos;


 
    private void Start()
    {
        boundsRef = GetComponent<Collider>().bounds;
        activePos = transform.position - new Vector3(0, boundsRef.extents.y, 0);
        unActivePos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(Time.fixedTime - time >=1)
        activated = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        activated = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        time = Time.time;
        activated = false;
    }

    private void FixedUpdate()
    {
        Activate(activated);
    }

    void Activate(bool _activated)
    {
        if (_activated)
        {
            transform.position = Vector3.Lerp(unActivePos, activePos, Time.time - time);

        }

        else
        {
            transform.position = Vector3.Lerp(activePos, unActivePos, Time.time - time);
        }
    }
}
