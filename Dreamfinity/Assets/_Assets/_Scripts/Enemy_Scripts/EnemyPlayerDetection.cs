using UnityEngine;


public class EnemyPlayerDetection : MonoBehaviour {

	 GameObject playerRef;
	private EnemyController m_enemyController;
	public float fieldOfViewAngle;
    public float headRotMaxAngle;
    SphereCollider col;
    public GameObject m_headObjRef;
    public Transform headReff,playerTrans;
    Vector3 direction;

    public bool playerInSight;
    GameObject targetSheep;

    void Awake()
    {
        m_enemyController =  transform.parent.GetComponent<EnemyController>();
        col = GetComponentInChildren<SphereCollider>();
        m_headObjRef = transform.GetChild(0).gameObject;
        playerRef = GameObject.FindWithTag("Player");

    }
    void Start ()
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
        if (other.gameObject.transform.tag == "Player")
        {
            RaycastHit hit;

            if (Physics.Raycast(headReff.position, direction, out hit))
            {
                if (hit.collider.gameObject.transform.tag == "Player")
                {
                    m_enemyController.playerInSight = true;
                    m_enemyController.lastPlayerSighting = playerRef.transform.position;
                    PlayerInSight();
                }
                else
                {
                    m_enemyController.playerInSight = false;
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
