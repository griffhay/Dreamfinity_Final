using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepDetection : MonoBehaviour {

    GameObject playerRef;
    private SheepNavMeshControl sheepNavMeshCont;
    SphereCollider col;
    public GameObject m_headObjRef;
    public Transform headReff, playerTrans;
    Vector3 playerDirection;
    Vector3 goalDirection;

    void Awake()
    {
        sheepNavMeshCont = gameObject.transform.GetComponent<SheepNavMeshControl>();
        col = GetComponent<SphereCollider>();
        playerRef = GameObject.FindWithTag("Player");
        
    }

    void Start()
    {
        m_headObjRef = this.transform.GetChild(0).gameObject;
        headReff = m_headObjRef.transform;
    }

    public void Update()
    {
        headReff = m_headObjRef.transform;
       
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "SheepGoal")
        {
            goalDirection = other.gameObject.transform.position - headReff.position;
            RaycastHit hit;

            if (Physics.Raycast(headReff.position, goalDirection, out hit))
            {
                if (hit.collider.gameObject.transform.tag == "SheepGoal")
                {
                    sheepNavMeshCont.playerInSight = false;
                    sheepNavMeshCont.goalInSight = true;
                    sheepNavMeshCont.goalPosition = hit.collider.gameObject.transform.position;
                }
                else
                {
                    sheepNavMeshCont.goalInSight = false;
                }
            }
        }
        else if (other.gameObject.transform.tag == "Player")
        {

            playerTrans = playerRef.transform;
            playerDirection = playerTrans.position - headReff.position;

            RaycastHit hit;

            if (Physics.Raycast(headReff.position, playerDirection, out hit))
            {
                if (hit.collider.gameObject.transform.tag == "Player")
                {
                    sheepNavMeshCont.playerInSight = true;
                    sheepNavMeshCont.lastPlayerSighting = playerRef.transform.position;
                }
                else
                {
                    sheepNavMeshCont.playerInSight = false;
                }
            }
        }
    }
}
//if(hit.collider.gameObject.transform.tag == "SheepGoal")
//{
//    sheepNavMeshCont.goalInSight = true;
//    sheepNavMeshCont.goalPosition = hit.collider.gameObject.transform.position;
//}
//else
//{
//    sheepNavMeshCont.goalInSight = false;
//}