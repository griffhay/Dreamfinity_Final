using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyControl : MonoBehaviour {

    public enum EnemyState {Idle, Patrol, Wondering, Persuing}

    /*Referances*/
    NavMeshAgent m_navAgent;
    EnemyDetection m_enemyDetection;
    Rigidbody m_rigBodyRef;

    [Header("Enemy Info")]
    public EnemyState m_enemyState, m_previousState;
    public int enemyToughness;

    Vector3 m_guardPost;

    public GameObject m_waypointParentObj;
    public int m_currentWaypointIntex;
    public GameObject m_currentWayponint;
    float m_dist;

    [Header("Player Detection")]
    public bool playerInSight;
    public Vector3 m_lastPlayerSighting;

    //Refferences out side of this game object
    GameObject m_playerRef;

    public void Start()
    {    
        m_navAgent = GetComponent<NavMeshAgent>();
        m_enemyDetection = GetComponent<EnemyDetection>();
        m_rigBodyRef = GetComponent<Rigidbody>();
        m_playerRef = GameObject.FindWithTag("Player");

        if(m_waypointParentObj == null)
        {
            m_waypointParentObj = new GameObject();
        }

        if(m_enemyState == EnemyState.Idle)
        {
            m_guardPost = transform.position;
        }

        m_currentWaypointIntex = 0;

        
    }

    private void Update()
    {
        EnemyStateMachine();

    }

    public void EnemyStateMachine()
    {

        if(m_enemyState == EnemyState.Idle)
        {
            if(m_navAgent.transform.position != m_guardPost)
            {
                m_navAgent.destination = m_guardPost;
            }

            if (playerInSight)
            {
                m_previousState = EnemyState.Idle;
                m_enemyState = EnemyState.Persuing;

            }
        }

        if(m_enemyState == EnemyState.Persuing)
        {
            m_navAgent.destination = m_lastPlayerSighting;

            if(!playerInSight)
            {
                if(m_navAgent.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    m_enemyState = m_previousState;
                }
            }
        }

        if (m_enemyState == EnemyState.Patrol)
        {

            m_currentWayponint = m_waypointParentObj.transform.GetChild(m_currentWaypointIntex).gameObject;
            Debug.Log(m_currentWayponint);

            m_navAgent.destination = m_currentWayponint.transform.position;

            if (m_navAgent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                if (m_currentWaypointIntex >= m_waypointParentObj.transform.childCount - 1)
                {
                    m_currentWaypointIntex = 0;
                }
                else
                {
                    m_currentWaypointIntex += 1;

                    m_navAgent.SetDestination(m_currentWayponint.transform.position);
                }

                m_currentWayponint = m_waypointParentObj.transform.GetChild(m_currentWaypointIntex).gameObject;
            }
        }
    }

}
