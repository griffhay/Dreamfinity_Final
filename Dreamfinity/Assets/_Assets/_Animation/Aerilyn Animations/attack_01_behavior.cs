﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_01_behavior : StateMachineBehaviour
{
    CharacterControler charContRef;
    Rigidbody rigBodRef;
    float steDistance;
    AttackAnimEvent attackAnimEventRef;

    public float stepDistance;
    public float stepTime;

    void Awake()
    {
        charContRef = GameObject.FindWithTag("Player").GetComponent<CharacterControler>();
        rigBodRef = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        attackAnimEventRef = GameObject.FindWithTag("Player").GetComponent<AttackAnimEvent>();
    }



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        charContRef.isAttacking = true;
        rigBodRef.velocity = Vector3.zero;
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

     
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        charContRef.isAttacking = false;
        attackAnimEventRef.startStep = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {


    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}