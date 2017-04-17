using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionDoor : MonoBehaviour {

    public int Threshold;
    public bool on;
    SheepCounterManager sheepCountRef;

    private void Start()
    {
        sheepCountRef = GameObject.FindWithTag("SheepCounterText").GetComponent<SheepCounterManager>();
        on = false;
    }


    // Update is called once per frame
    void Update () {
	
        if (sheepCountRef.sheepCount > Threshold)
        {
            on = true;
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            if (on == true)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
