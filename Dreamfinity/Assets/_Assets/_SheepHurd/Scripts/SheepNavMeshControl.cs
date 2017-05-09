using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepNavMeshControl : MonoBehaviour {

    public bool goalInSight;
    public Vector3 goalPosition; //the position of the goal in sight

    public bool playerInSight;
    public Vector3 lastPlayerSighting;
    private NavMeshAgent NavMeshAgentRef;

    private void Awake()
    {
        NavMeshAgentRef = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(goalInSight)
        {
            NavMeshAgentRef.SetDestination(goalPosition);

        }
        else if (playerInSight)
        {
            NavMeshAgentRef.SetDestination(lastPlayerSighting);
        }
    }
}
