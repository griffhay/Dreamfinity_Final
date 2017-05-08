using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenBoundsManager : MonoBehaviour {

    public bool playerPressent;
    GameObject penRef;
    SheepPenController sheepPenCont;

    bool countSheep;
    int sheepCount;
    GameObject[] Sheeps;

    private void Awake()
    {
        penRef = transform.parent.gameObject;
    }

    private void Start()
    {
        sheepPenCont = transform.parent.gameObject.GetComponent<SheepPenController>();  
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            playerPressent = true;
        }
        else
        {
            playerPressent = false;
        }

        
    }
}
