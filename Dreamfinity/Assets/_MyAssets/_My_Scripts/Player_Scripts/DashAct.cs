using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class DashAct : MonoBehaviour {

    /*Empowered Dash Variables*/
    public float m_dashSpeed = .5f;
    public bool m_isDashing;

    float timer;
    CharacterControler m_charContRef;
    Rigidbody m_rigBodRef;

    public float m_dashForce;
    LucidityControl playerLucRef;
    ParticleSystem particleRef;

    private void Start()
    {
        if (m_dashForce == 0)
        {
            m_dashForce = 1000f;
        }

        m_rigBodRef = GetComponent<Rigidbody>();
        particleRef = GameObject.Find("Empowered_DashEffect").GetComponent<ParticleSystem>();
        playerLucRef = GameObject.FindWithTag("Player").GetComponent<LucidityControl>();
        /*Empowered Dash Variable Initialization*/
        m_charContRef = GetComponent<CharacterControler>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && !m_isDashing )
        {

            if (Input.GetButtonDown("Dash") && playerLucRef.balance > 0)
            {
                m_isDashing = true;
                timer = Time.time;
                playerLucRef.Withdraw(1);
                particleRef.Play();
                //play audio
                AudioSource audio = GameObject.FindWithTag("AudioDash").GetComponent<AudioSource>();
                audio.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        if (m_isDashing)
        {

            if (Time.time - timer <= m_dashSpeed)
            {
                //m_charContRef.m_dashing = true;
                m_rigBodRef.AddForce(playerLucRef.transform.forward * m_dashForce, ForceMode.Impulse);
            }
            else
            {
                //m_charContRef.m_dashing = false;
                particleRef.Stop();
                m_isDashing = false;
            }
        }
    }

    void Dash(Rigidbody _rigBod, Vector3 dashDir)
    {
        _rigBod.AddForce(dashDir);
    }
}
