using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyAnimationController : MonoBehaviour
{
    Animator animatorRef;
    NavMeshAgent navMeshAgentRef;

    int movingHash = Animator.StringToHash("Moving");
    int attackHash = Animator.StringToHash("Attack");
    int grabHash = Animator.StringToHash("Grab");
    int hitHash = Animator.StringToHash("Hit");

    void Awake()
    {
        animatorRef = GetComponent<Animator>();
        navMeshAgentRef = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        if (navMeshAgentRef.velocity != Vector3.zero)
        {
            animatorRef.SetBool(movingHash, true);
        }
        else
        {
            animatorRef.SetBool(movingHash, false);

        }

    }
}