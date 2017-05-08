using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C_Animations;

public class WeaponControl : MonoBehaviour {

    CharacterAnimationControl charAnimContRef;
    Animator charAnimatiorRef;
    GameObject playerRef;
    AttackAnimEvent attackAnimEventRef;
    Collider weaponCollier;

    int attackHash = Animator.StringToHash("Attack");
    public bool hit;
    public bool isAttacking;

    public void Awake()
    {
        playerRef = GameObject.FindWithTag("Player");
        charAnimContRef = playerRef.GetComponent<CharacterAnimationControl>();
        charAnimatiorRef = playerRef.GetComponent<Animator>();
        attackAnimEventRef = playerRef.GetComponent<AttackAnimEvent>();
        weaponCollier = GetComponent<Collider>();
    }

    private void Start()
    {
        
        hit = false;  
    }

    private void Update()
    {
        isAttacking = attackAnimEventRef.isAttacking;

        if(isAttacking)
        {
            weaponCollier.enabled = true;
        }

        if(!isAttacking)
        {
            weaponCollier.enabled = false;
        }

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
                other.SendMessage("TagYoureIt");
                hit = true;
            }
           
        }
    }
    
    
}
