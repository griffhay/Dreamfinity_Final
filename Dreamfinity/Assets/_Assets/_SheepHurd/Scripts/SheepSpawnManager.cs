using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawnManager : MonoBehaviour {

    public SheepPenController sheepPenCont;
    public Transform[] sheepSpawnList;
    public GameObject[] sheeps;
    public GameObject sheepRef;

    private void Start()
    {
        sheepPenCont = GetComponent<SheepPenController>();
        sheepRef = Resources.Load("pfb_Sheep") as GameObject;
        SheepArrayInit();
    }

    private void SheepArrayInit()
    {
        sheepSpawnList = new Transform[this.transform.childCount];
        sheeps = new GameObject[this.transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            sheepSpawnList[i] = this.transform.GetChild(i);
            sheeps[i] = this.transform.GetChild(i).gameObject;
        }

        sheepPenCont.sheeps = sheeps;
    }

    public void SpawnFlock()
    {
        GameObject sheep = sheepRef;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            sheep = Instantiate(sheepRef, sheepSpawnList[i].position, sheepSpawnList[i].rotation);
            sheep.GetComponent<SheepController>().spawnIndex = i;
            sheep.GetComponent<SheepController>().parentPen = gameObject;
        }
    }

    public void SpawnSheep(int i)
    {
        GameObject sheep = sheepRef;
        sheep = Instantiate(sheepRef, sheepSpawnList[i].position, sheepSpawnList[i].rotation);
        sheep.GetComponent<SheepController>().spawnIndex = i;
        sheep.GetComponent<SheepController>().parentPen = gameObject;
    }
}
