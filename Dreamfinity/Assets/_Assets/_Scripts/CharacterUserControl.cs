using System;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using Invector.CharacterController;
using C_Utilities;



public class CharacterUserControl : MonoBehaviour
{

    private v3rdPersonCamera camController;   // A reference to the main camera in the scenes transform
    private CharacterControler charCont;
    private DrainLucidityAct drainLucAct; 
    private SpendLucidityAct SpendLucAct;

    private Transform m_Cam;
    private GameObject m_camObject;
    private Vector3 m_CamForward;    // The current forward direction of the camera
    private Vector3 m_Move;    // the world-relative desired move direction, calculated from the camForward and user input.
    private Vector2 m_MouseInput;    // Values 0 - 1 for mouse movement. 0 when the mouse is moving and 1 or -1 when the 
    private bool m_jump;    //Jumping or not
    private bool m_crouch; // Is the player Crouching?
    private bool m_drain, m_spend; // Booleans that are true when the player is draining or spending RES.
    private bool m_attack;
    private bool m_lucJump, m_lucAttack, m_lucDash;

    private bool getAxisDown;

    Trigger paused;

    private void Awake()
    {
        charCont = GetComponent<CharacterControler>();
        camController = GameObject.FindWithTag("MainCamera").GetComponent<v3rdPersonCamera>();
        drainLucAct = GetComponent<DrainLucidityAct>();
        SpendLucAct = GetComponent<SpendLucidityAct>();
        m_Cam = GameObject.FindWithTag("MainCamera").transform;
        //pauseMenuControl = GameObject.FindWithTag("PauseMenu").GetComponent<PauseMenuControl>();
    }

    private void Start()
    {
        paused.Reset();
        getAxisDown = false;
    }

    public void Update()
    {
        if (Input.GetAxisRaw("Spend") == 1)
        {
            if (!getAxisDown)
            {
                SpendLucAct.Cast(true);
                getAxisDown = true;

            }
        }
        else if (Input.GetAxisRaw("Spend") == 0)
        {
            getAxisDown = false;
        }

        if (!m_jump)
        {
           m_jump = Input.GetButton("Jump");
        }


        SpendLucAct.Cast(m_spend);
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        m_lucJump = Input.GetButton("Jump");
        m_drain = Input.GetButton("Drain");
        m_attack = Input.GetButton("Attack");
        m_spend = Input.GetButtonDown("Spend");

        Vector2 mouseAxis;
        mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
        m_Move = v*m_CamForward + h*m_Cam.right;
           
        charCont.Move(m_Move, m_crouch, m_jump);
        camController.RotateCamera(mouseAxis.x, mouseAxis.y);

        m_jump = false;

    }
}

