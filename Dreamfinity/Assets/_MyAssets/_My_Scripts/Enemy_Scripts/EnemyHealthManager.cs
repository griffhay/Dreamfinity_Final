using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {


    public int helthMax;
    public int heathValue = 1;

    GameObject lucObjRef;

    Rigidbody rigbodRef;

    float deathTime;
    public bool dying;

    private void Start()
    {
        heathValue = helthMax;

        lucObjRef = Resources.Load("pfb_RescourseCast") as GameObject;

        rigbodRef = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if(heathValue <= 0)
        {
            Instantiate(lucObjRef, transform.position + Vector3.up, transform.rotation);
            Instantiate(lucObjRef, transform.position + Vector3.up, transform.rotation);
            Instantiate(lucObjRef, transform.position + Vector3.up, transform.rotation);

            GameObject.Destroy(this.gameObject);
        }

      
    }


}
