using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]


public class WellControl : MonoBehaviour {

    public LucidityControl m_resCont;
    public LucidityControl m_plrResCont;

    public bool m_draining; // True when the well is being drained
    GameObject m_castingObj;
    public float distance;

    GameObject playerRef;

    ParticleSystem m_DrainEffect;

    void Start()
    {
        m_DrainEffect = GetComponent<ParticleSystem>();
        m_plrResCont = GameObject.FindWithTag("Player").GetComponent<LucidityControl>();
        m_resCont = GetComponent<LucidityControl>();
        m_DrainEffect.IsAlive();
        playerRef = GameObject.FindWithTag("Player");
        m_castingObj = Resources.Load("pfb_RescourseCast") as GameObject;
    }

    void Update()
    {
        if(!m_draining)
        {
            m_DrainEffect.Stop();  
        }

        if( Vector3.Distance(transform.position + Vector3.up, playerRef.transform.position) < distance && Input.GetButton("Drain"))
        {
            GameObject newLuc = Instantiate(m_castingObj, transform.position + Vector3.up , transform.rotation);
            newLuc.GetComponent<LucidityControl>().balance = m_plrResCont.limit - m_plrResCont.balance;
            Vector3 lucDir = playerRef.transform.position - newLuc.transform.position;
            newLuc.GetComponent<Rigidbody>().velocity = lucDir;
            //audio manager
            AudioSource audio = GameObject.FindWithTag("AudioWellDrain").GetComponent<AudioSource>();
            audio.Play();
           
        }

        m_draining = false;
    }
}
