using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAct : MonoBehaviour {


    /*Refferances*/
    CharacterControler m_charContRef;
    Rigidbody m_rigBodRef;
    LucidityControl m_playerLucRef;
    ParticleSystem particleRef;
    Animation heatedSlippersAnim;

    /*Empowered Dash Variables*/
    public float m_dashSpeed = .5f;
    public bool m_isDashing;
    public float m_dashForce;
    public float m_coolDownDeration;  //The amount of time it takes for the dash to cool down

    /*Dash derations and timers*/
    public float m_timer;    //The amount of time the player will dash when the button is pressed
    public float m_coolDownTimer;    //Holds the time for the dash cool down
    public bool m_cooling;   //True when the players dash ability is cooling down
         
    private void Awake()
    {
        m_rigBodRef = GetComponent<Rigidbody>();
        particleRef = GameObject.Find("Empowered_DashEffect").GetComponent<ParticleSystem>();
        m_playerLucRef = GameObject.FindWithTag("Player").GetComponent<LucidityControl>();
        m_charContRef = GetComponent<CharacterControler>();
    }

    public void Start()
    {
        if (m_dashForce == 0)
        {
            Debug.LogAssertion("There was no value assigned to the DashAct scrpit attached to " + gameObject.name);
        }
    }

    void Update()
    {

        if (m_cooling)
        {
            if (m_coolDownTimer < m_coolDownDeration)
            {
                m_coolDownTimer += 1 * Time.deltaTime;
                m_cooling = true;
            }
            else
            {
                m_cooling = false;
            }
        }

        

        if (m_cooling == false && m_charContRef.m_isGrounded)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && !m_isDashing)
            {

                if (Input.GetButtonDown("Dash"))//MAY WANT TO SWITHC TO A GetButton INSTEAD
                {
                    m_isDashing = true;
                    m_timer = Time.time;
                    particleRef.Play();
                }
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (m_isDashing)
        {

            if (Time.time - m_timer <= m_dashSpeed)
            {
                m_charContRef.m_dashing = true;
                m_rigBodRef.AddForce(m_playerLucRef.transform.forward * m_dashForce, ForceMode.Impulse);
            }
            else
            {
                m_charContRef.m_dashing = false;
                particleRef.Stop();
                m_coolDownTimer = 0;
                m_cooling = true;
                m_isDashing = false;
            }
        }
    }

    void Dash(Rigidbody _rigBod, Vector3 dashDir)
    {
        _rigBod.AddForce(dashDir);
    }
}
