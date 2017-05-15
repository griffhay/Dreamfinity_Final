using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;

public class AimLucidity : MonoBehaviour {

    v3rdPersonCamera camControlRef;
    GameObject camObjRef;
    Camera camRef;
    CharacterControler charContRef;
    bool preAim;
    GameObject paintBrush;
    Transform paintBrishAimTransRef;


    public void Awake()
    {
        camControlRef = GameObject.FindWithTag("MainCamera").GetComponent<v3rdPersonCamera>();
        charContRef = GameObject.FindWithTag("Player").GetComponent<CharacterControler>();
        camObjRef = (GameObject.FindWithTag("MainCamera"));
        camRef = camObjRef.GetComponent<Camera>();
    }

    public void Start()
    {
        preAim = true;
    }
    void Update()
    {
        Aim(Input.GetAxisRaw("Aim"));
       
    }

    public void Aim(float aimAxisValue)
    {
        if(aimAxisValue != 0)
        {
            preAim = false;
            
            charContRef.ToggleCharacterlook(true);
            camControlRef.defaultDistance = 1f;
            camControlRef.rightOffset = 0.7f;
            camControlRef.height = .6f;

        }
        else
        {
            if(!preAim)
            {
                charContRef.ToggleCharacterlook(false);
                camControlRef.defaultDistance = 7;
                camControlRef.rightOffset = 0.25f;
                camControlRef.height = 1f;
            }
            
        }
        
    }
        
}
