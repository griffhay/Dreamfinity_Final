using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    NavMeshAgent m_navMeshAgentRef;
    GameObject m_playerRef;

    public bool playerInSight;
    public Vector3 lastPlayerSighting;

    void Start () {
        m_navMeshAgentRef = GetComponent<NavMeshAgent>();
        m_playerRef = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
		if(playerInSight)
        {
            m_navMeshAgentRef.SetDestination(lastPlayerSighting);
        }
        
	}
}
