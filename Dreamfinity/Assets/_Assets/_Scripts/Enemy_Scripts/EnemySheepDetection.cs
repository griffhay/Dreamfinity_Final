using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheepDetection : MonoBehaviour {

    GameObject playerRef;
    private EnemyController m_enemyController;
    public float fieldOfViewAngle;
    public float headRotMaxAngle;
    SphereCollider col;
    public GameObject m_headObjRef;
    public Transform headReff, playerTrans;
    Vector3 direction;

    public bool playerInSight;
    GameObject targetSheep;

    void Awake()
    {
        m_enemyController = transform.parent.GetComponent<EnemyController>();
        col = GetComponentInChildren<SphereCollider>();
        m_headObjRef = transform.GetChild(0).gameObject;
        playerRef = GameObject.FindWithTag("Player");

    }
    void Start()
    {

        headReff = m_headObjRef.transform;
        playerTrans = playerRef.transform;
    }


    public void Update()
    {

        direction = playerTrans.position - headReff.position;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Sheep")  // if a sheep is within range
        {
            direction = other.gameObject.transform.position - headReff.position;
            RaycastHit hit;

            if (Physics.Raycast(headReff.position, direction, out hit)) //if the sheep in range is also in line of sight
            {
                if (hit.collider.gameObject.transform.tag == "Sheep")
                {
                    m_enemyController.sheepInSight = true;
                    m_enemyController.targetSheep = hit.collider.gameObject;

                }
                else //the sheep is not in sight
                {
                    m_enemyController.sheepInSight = false;
                }
            }
        }
    }
    

    void PlayerInSight()
    {
        Quaternion headRot = Quaternion.LookRotation(playerRef.transform.position - headReff.transform.position, Vector3.up);
        headReff.rotation = headRot;
    }
}

