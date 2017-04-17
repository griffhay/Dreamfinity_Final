using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpAct : MonoBehaviour {

    GameObject playerRef;
    CharacterControler charContRef;
    LucidityControl playerLucContRef;
    Rigidbody rigBodRef;

    public ParticleSystem particleRef;

    public bool doubleJumpFlag;
    public bool isDblJumping;
    public bool isJumping;

    public float jumpDeration;
    public float timer;
    public float doubleJumpForce;

    private void Start()
    {
        playerRef = GameObject.FindWithTag("Player");
        charContRef = playerRef.GetComponent<CharacterControler>();
        playerLucContRef = playerRef.GetComponent<LucidityControl>();

        rigBodRef =  playerRef.GetComponent<Rigidbody>();
        doubleJumpFlag = false;
        isDblJumping = false;


    }

    private void Update()
    {   
         if(charContRef.m_isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if(isJumping && Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            doubleJumpFlag = true;
        }

        if(doubleJumpFlag)
        {
            if (Input.GetButtonDown("Jump") && playerLucContRef.balance >= 1 && doubleJumpFlag)
            {
                isDblJumping = true;
                timer = Time.time;
                playerLucContRef.Withdraw(1);
            }

            if (Input.GetButton("Jump") && isDblJumping)
            {
                if (Time.time - timer < jumpDeration)
                {
                    //rigBodRef.useGravity = false;
                    playerRef.GetComponent<Rigidbody>().AddForce(Vector3.up * doubleJumpForce, ForceMode.Acceleration);
                }
                else
                {
                    isJumping = false;
                    isDblJumping = false;
                    doubleJumpFlag = false;
                }  
            }

            if (Input.GetButtonUp("Jump") && isDblJumping)
            {
                isJumping = false;
                isDblJumping = false;
                doubleJumpFlag = false;
            }

            if(!doubleJumpFlag)
            {
                //rigBodRef.useGravity = true;
            }

            if (charContRef.m_isGrounded)
            {
                isJumping = false;
                isDblJumping = false;
                doubleJumpFlag = false;
            }
        }   
    }
}
