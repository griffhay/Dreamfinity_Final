using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepGoalControl : MonoBehaviour {

    SheepCounterManager sheepCountMngRef;

    private void Awake()
    {
        sheepCountMngRef = GameObject.FindWithTag("SheepCounterText").GetComponent<SheepCounterManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.transform.tag == "Sheep")
        {
            GameObject sheep = collision.collider.gameObject;
            sheepCountMngRef.sheepCount += 1;
            //sheepControler.parentPen.GetComponentInChildren<SheepSpawnManager>().SpawnSheep(sheepControler.spawnIndex);
            GameObject.Destroy(collision.collider.gameObject);
        }
    }
}
