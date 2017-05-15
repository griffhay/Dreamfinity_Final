using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {


    public int spawnPoint;
    RespawnManager respawnRef;



	// Use this for initialization
	void Start () {

        respawnRef = GameObject.FindWithTag("Player").GetComponent<RespawnManager>();

	}

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.gameObject.transform.tag == "Player")
        {
            respawnRef.spawnLocation = spawnPoint;
            respawnRef.SaveSpawn();
        }
    }
}
