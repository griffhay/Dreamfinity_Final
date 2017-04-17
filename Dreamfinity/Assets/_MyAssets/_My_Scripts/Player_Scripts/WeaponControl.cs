using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C_Animations;

public class WeaponControl : MonoBehaviour {

    CharacterAnimationControl charAnimContRef;
    Animator charAnimatiorRef;
    GameObject playerRef;
    AttackAnimEvent attackAnimEventRef;

    int attackHash = Animator.StringToHash("Attack");
    public bool hit;
    public bool isAttacking;
    //public bool isAttacking;
    
    private void Start()
    {
        playerRef = GameObject.FindWithTag("Player");
        charAnimContRef = playerRef.GetComponent<CharacterAnimationControl>();
        charAnimatiorRef = playerRef.GetComponent<Animator>();
        attackAnimEventRef = playerRef.GetComponent<AttackAnimEvent>();
        hit = false;  
    }

    private void Update()
    {
        isAttacking = attackAnimEventRef.isAttacking;
        if(hit && !isAttacking)
        {
            hit = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (isAttacking && !hit)
            {
                other.gameObject.GetComponent<EnemyHealthManager>().heathValue -= 1;
                hit = true;
            }
           
        }
    }
    
    
}
