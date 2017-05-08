using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepPenController : MonoBehaviour {

    GameObject penDoorRef;
    bool isOpen; //True when the pen door is oppen;
    public SheepSpawnManager sheepSpawnMng;

    public GameObject[] sheeps;
    bool firstFrame;

    public void Awake()
    {
        sheepSpawnMng = GetComponent<SheepSpawnManager>();
    }

    public void Start()
    {
        sheepSpawnMng.SpawnFlock();
    }


}
