using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class LucidityProjectile : MonoBehaviour {

    LucidityControl m_plrResCont;
    GameObject m_playerRef;
    Rigidbody m_rigBodRef;
    float timeSinceBirth; //float value to track how long this is alive
    public float lifeSpan = 10f; //length of time this object can be alive before destroying its self
    int bounces;
    int threshold = 5;
    public float m_decelForc; //The amount of deceleration when the projectile collides with something;
    Collider IgnoredCollision;
    bool ignoringCollider;
    float ignoreColliderTimer;
    float ignoreColliderTimeSpan = .5f;

    private void Awake()
    {
        m_rigBodRef = GetComponent<Rigidbody>();
        m_playerRef = GameObject.FindWithTag("Player");
    }
    public void Start()
    {
        m_plrResCont = m_playerRef.GetComponent<LucidityControl>();
        timeSinceBirth = Time.time;

    }

    public void Update()
    { 
        if(ignoringCollider)
        {
            ignoreColliderTimer += 1 * Time.deltaTime;

            if(ignoreColliderTimer >= ignoreColliderTimeSpan)
            {
                Physics.IgnoreCollision(IgnoredCollision, GetComponent<Collider>(), false);
                ignoringCollider = false;
            }
        }



        if (Time.time - timeSinceBirth > lifeSpan || Input.GetKey(KeyCode.F1))
        {
            Destroy(this.gameObject);   
        }
    }

    void OnCollisionEnter(Collision other)
    {



        if (other.collider.gameObject.transform.tag == ("Player"))
        {
            if(m_plrResCont.balance < m_plrResCont.limit)
            {
                //play audio
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
                m_plrResCont.Deposit(1);

                Destroy(this.gameObject);
            }
        }
        else
        {
            if(bounces == 0)
            {
                m_rigBodRef.useGravity = true;
            }
           
            bounces++;

            if (bounces >= threshold)
            {
                Destroy(this.gameObject);
            }
        }

        
    }

    public void OnTakeDamage(int amountOfD, Collider ignoredCollider)
    {
        m_rigBodRef.useGravity = true;
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.value, 1, Random.value) * 10, ForceMode.Impulse);
        Physics.IgnoreCollision(ignoredCollider, GetComponent<Collider>());
        IgnoredCollision = ignoredCollider;
        ignoringCollider = true;
        ignoreColliderTimer = 0;

    }

}
