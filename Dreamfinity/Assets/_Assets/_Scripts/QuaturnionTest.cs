using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaturnionTest : MonoBehaviour {

    public Quaternion testQuaturnion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = testQuaturnion;
	}
}
