using UnityEngine;
using System.Collections.Generic;
namespace C_Animations
{

    public class CharacterAnimationControl : MonoBehaviour
    {
        Animator c_animator;
        CharacterControler charContRef;
        int attackHash = Animator.StringToHash("Attack");
        public bool Attack01;
        public bool Movement;
        public bool Idle;
        int MoveHash = Animator.StringToHash("Movement");
        int endAttack = 0;



       //AnimationEvent  
       

        void Start()
        {
            c_animator = GetComponent<Animator>();
            charContRef = GameObject.FindWithTag("Player").GetComponent<CharacterControler>();
            c_animator.ResetTrigger(attackHash);
        }

        void Update()
        {
            C_AttackState();

            Movement = MovingCheck(Input.GetAxisRaw("Horizontal") !=0, Input.GetAxis("Vertical") != 0);
            c_animator.SetBool(MoveHash, Movement);
            c_animator.SetBool("IsGrounded", charContRef.m_isGrounded);
            c_animator.SetBool("Jump", charContRef.m_isJumping);
        }

        bool MovingCheck(bool hInput, bool vInput)
        { 

            if(hInput || vInput)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void C_AttackState()
        {


            if (Input.GetButtonDown("Attack"))
            {
                c_animator.SetTrigger(attackHash);
                Attack01 = true;
               
            }

        }

    }
}



/*
 
        float MovingCheck(float hInput, float vInput)
        {
            float moveAverage = (hInput + vInput) / 2;

        return moveAverage;
        }
     */
