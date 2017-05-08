using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public enum EnemyState { Idle, ChasingPlayer, ChasingSheep, GrabingSheep, ThrowingSheep, Retreating, DropingSheep}

    NavMeshAgent m_navMeshAgentRef;
    public GameObject m_playerRef;
    Rigidbody rigBodRef;

    //Player Detection
    public bool playerInSight;
    public Vector3 lastPlayerSighting;
    public bool isChasingPlayer;

    //SheepDetection
    public bool sheepInSight;
    public GameObject targetSheep;
    public bool isChasingSheep;
    public bool retreat;
    public GameObject homeBase;

    //Animation
    public Animator animRef;
    public EnemyHealthManager enimHealth;

    //Animator Hash IDs
    int movingHash = Animator.StringToHash("Moving");
    int attackHash = Animator.StringToHash("Attack");
    int grabHash = Animator.StringToHash("Grab");
    int hitHash = Animator.StringToHash("Hit");
    int throwHash = Animator.StringToHash("ThrowSheep");

    [Header("Sheep Animation State Debug(Do Not Change)")]
    public bool grabSheep;
    public bool isGrabbingSheep;
    public bool throwSheep;
    public bool isThrowingSheep;
    public bool hasSheep;

    [Header("EnemyState")]
    public EnemyState enemyState;

    GameObject sheepObj;

    EnemyAnimationEvent enemyAnimEvent;
    public int heldSheepIndex;
    SheepSpawnManager sheepSpawnManager;
    GameObject hiddenSheep;
    Transform holdPosition;
    GameObject thrownSheep;
    GameObject droppedSheep;

    public float angleToPlayer;

    public SphereCollider hitCollider;


    public float distanceToPlayer, distanceToBase, distanceToTargetSheep;


    public void Awake()
    {
        animRef = GetComponent<Animator>();
        rigBodRef = GetComponent<Rigidbody>();
        enimHealth = GetComponent<EnemyHealthManager>();
        m_playerRef = GameObject.FindWithTag("Player");
        enemyAnimEvent = GetComponent<EnemyAnimationEvent>();
        hiddenSheep = transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject;
        holdPosition = transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        hitCollider = holdPosition.gameObject.GetComponent<SphereCollider>();
    }

    void Start ()
    {

        thrownSheep = Resources.Load("pfb_ThrownSheep") as GameObject;
        droppedSheep = Resources.Load("pfb_Sheep") as GameObject;
        m_navMeshAgentRef = GetComponent<NavMeshAgent>();
        //sheepSnatch = GetComponent<EnemySheepSnatchAct>();
        grabSheep = throwSheep = false;
        hiddenSheep.GetComponent<MeshRenderer>().enabled = false;
        enemyState = EnemyState.Idle;

        GameObject sheepObj = Resources.Load("pfb_Sheep") as GameObject;
        hitCollider.enabled = false;
    }

    void FixedUpdate()
    {

        EnemyStateMachine();

        distanceToPlayer = Vector3.Distance(m_playerRef.transform.position, transform.position);
        distanceToBase = Vector3.Distance(homeBase.transform.position, transform.position);
        grabSheep = enemyAnimEvent.grabSheep;
        throwSheep = enemyAnimEvent.throwSheep;

        angleToPlayer = Vector3.Angle(transform.forward, m_playerRef.transform.position - transform.position);

    }

    void EnemyStateMachine()
    {
        switch(enemyState)
        {
            case EnemyState.Idle:
                throwSheep = false;
                enemyAnimEvent.throwSheep = false;

                if (sheepInSight)
                {
                    enemyState = EnemyState.ChasingSheep;

                }

                if(playerInSight)
                {
                    enemyState = EnemyState.ChasingPlayer;
                }

                animRef.SetBool(movingHash, false);

                break;

            case EnemyState.ChasingPlayer:

                if(sheepInSight && targetSheep)
                {
                    enemyState = EnemyState.ChasingSheep;
                }

                if(!playerInSight)
                {
                    if (m_navMeshAgentRef.remainingDistance == 0 && m_navMeshAgentRef.remainingDistance != Mathf.Infinity && m_navMeshAgentRef.pathStatus == NavMeshPathStatus.PathComplete)
                    {
                        enemyState = EnemyState.Idle;
                    }
                    
                }

                m_navMeshAgentRef.SetDestination(lastPlayerSighting);
                isChasingPlayer = true;

                if (distanceToPlayer < 2f)
                {
                    if(angleToPlayer < 25f)
                    {
                        animRef.SetTrigger(attackHash);
                    }
                }

                break;

            case EnemyState.ChasingSheep:

                float distance = Vector3.Distance(targetSheep.transform.position, transform.position);
                if (distance < 3f)
                {
                    enemyState = EnemyState.GrabingSheep;
                    animRef.SetTrigger(grabHash);
                    hasSheep = true;
                }

                if (!sheepInSight)
                {
                    if (playerInSight)
                    {
                        enemyState = EnemyState.ChasingPlayer;
                    }

                    if (m_navMeshAgentRef.remainingDistance == 0 && m_navMeshAgentRef.remainingDistance != Mathf.Infinity && m_navMeshAgentRef.pathStatus == NavMeshPathStatus.PathComplete)
                    {
                        enemyState = EnemyState.Idle;
                    }
                }

                m_navMeshAgentRef.SetDestination(targetSheep.transform.position);

                break;

            case EnemyState.GrabingSheep:

                if(grabSheep)
                {
                    if(targetSheep)
                    {
                        heldSheepIndex = targetSheep.GetComponent<SheepController>().spawnIndex;
                        sheepSpawnManager = targetSheep.GetComponent<SheepController>().parentPen.GetComponent<SheepSpawnManager>();
                        GameObject.Destroy(targetSheep.gameObject);
                        hiddenSheep.GetComponent<MeshRenderer>().enabled = true;
                        enemyState = EnemyState.Retreating;
                    }                   
                }

                break;

            case EnemyState.Retreating:
                grabSheep = false;
                enemyAnimEvent.grabSheep = false;
                m_navMeshAgentRef.destination = homeBase.transform.position;

                if (Vector3.Distance(homeBase.transform.position, transform.position) < 4f)
                {
                    enemyState = EnemyState.ThrowingSheep;
                    animRef.SetTrigger(throwHash);
                }

                break;

            case EnemyState.ThrowingSheep:

                if(enemyAnimEvent.throwSheep)
                {
                    sheepSpawnManager.SpawnSheep(heldSheepIndex);
                    GameObject dumbySheep = Instantiate(thrownSheep, hiddenSheep.transform.position, hiddenSheep.transform.rotation);

                    dumbySheep.GetComponent<Rigidbody>().velocity = (homeBase.transform.GetChild(0).transform.position - thrownSheep.transform.position);
                    hiddenSheep.GetComponent<MeshRenderer>().enabled = false;
                    enemyState = EnemyState.Idle;
                    hasSheep = false;
                }         
                
                break;

            case EnemyState.DropingSheep:

                hiddenSheep.GetComponent<MeshRenderer>().enabled = false;
                GameObject dropedSheep = Instantiate(droppedSheep, hiddenSheep.transform.position, hiddenSheep.transform.rotation);
                droppedSheep.GetComponent<SheepController>().spawnIndex = heldSheepIndex;
                droppedSheep.GetComponent<SheepController>().parentPen = sheepSpawnManager.gameObject;

                enemyState = EnemyState.Idle;

                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
         
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.transform.tag == "Player")
        {
            rigBodRef.isKinematic = true;
        }
        else
        {
            rigBodRef.isKinematic = false;
        }
    }

    public void TagYoureIt()
    {
        if(hasSheep)
        {
            enemyState = EnemyState.DropingSheep;
        }
        animRef.SetTrigger(hitHash);
        enimHealth.heathValue -= 1;

    }
}
 