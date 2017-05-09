using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lucidity;

public class PenDoorManager : MonoBehaviour {

    PlatformMovementControl moverRef;
    public bool isOpen;
    SheepPenController sheepPenCont;

    private void Awake()
    {
        moverRef = GetComponent<PlatformMovementControl>();
        sheepPenCont = transform.parent.gameObject.GetComponent<SheepPenController>();
    }

    private void Update()
    {
        if (moverRef.currentWayPointIndex == 0)
        {
            isOpen = false;
        }
        else
        {
            isOpen = true;
        }
    }
}
