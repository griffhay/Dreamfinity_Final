using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSheep : MonoBehaviour {

    SheepCounterManager sheepCounterManager;
	//gerardos sorry attempt to contribute to programming
	public int SheepValue;
    private void Awake()
    {
        sheepCounterManager = GameObject.FindWithTag("SheepCounterText").GetComponent<SheepCounterManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
		
        if(collision.collider.gameObject.transform.tag == "Player")
        {
			sheepCounterManager.sheepCount += SheepValue;
            //sheepCounterManager.sheepCount += 1;//old script keep this if other is broken and delete top line
            GameObject.Destroy(this.gameObject);
        }
    }
}
