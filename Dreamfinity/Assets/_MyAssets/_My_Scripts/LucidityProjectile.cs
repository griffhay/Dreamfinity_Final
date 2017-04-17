using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidityProjectile : MonoBehaviour {
    public LucidityControl m_plrResCont;
    Rigidbody m_rigBodRef;
    float timeSinceBirth; //float value to track how long this is alive
    public float lifeSpan = 10f; //length of time this object can be alive before destroying its self
    int bounces;
    int threshold = 3;
    public float m_decelForc; //The amount of deceleration when the projectile collides with something;


    private void Awake()
    {
        m_rigBodRef = GetComponent<Rigidbody>();
    }
    public void Start()
    {
        timeSinceBirth = Time.time;
        m_plrResCont = GameObject.FindWithTag("Player").GetComponent<LucidityControl>();
        
    }


    public void Update()
    { 
        if (Time.time - timeSinceBirth > lifeSpan || Input.GetKey(KeyCode.F1))
        {
            Destroy(this.gameObject);   
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        bounces++;
        if (other.collider.gameObject.transform.tag == ("Player"))
        {
            if(m_plrResCont.balance < m_plrResCont.limit)
            {
                m_plrResCont.Deposit(1);
                Destroy(this.gameObject);
            }
           
        }

        if (bounces >= threshold)
        {
            Destroy(this.gameObject);
        }
}

}
