using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpendLucidityAct : MonoBehaviour
{

    GameObject m_castingObj;
    LucidityControl m_resourceControl;
    public float m_castForce;
    public int m_castCost;

    //GameObject _casting;
    public Vector3 m_castPoint;
    public Camera m_cam;

    // Use this for initialization
    void Start()
    {
        m_resourceControl = GetComponent<LucidityControl>();
        m_cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        m_castingObj = Resources.Load("pfb_RescourseCast") as GameObject;
    }

    private void Update()
    {
        m_castPoint = GameObject.FindWithTag("Casting Point").transform.position;
    }

    public void Cast(bool casting)
    {
        GameObject _casting = m_castingObj;


        //Vector2 screenCenter = new Vector2(m_cam.pixelWidth / 2, m_cam.pixelHeight / 2);

        if (casting && m_resourceControl.balance > 0)
        {
            _casting = Instantiate(_casting, m_castPoint, transform.rotation);
            _casting.GetComponent<LucidityControl>().Deposit(m_castCost);
            _casting.GetComponent<Rigidbody>().AddForce(m_cam.transform.forward * m_castForce, ForceMode.Impulse);
            m_resourceControl.Deposit(-m_castCost);
        }
    }
}




