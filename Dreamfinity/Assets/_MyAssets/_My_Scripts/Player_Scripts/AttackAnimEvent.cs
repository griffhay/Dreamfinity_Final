using UnityEngine;
using System.Collections;

public class AttackAnimEvent: MonoBehaviour
{
    public bool isAttacking;


    public void AttackBeginEvent()
    {
        isAttacking = true;

       
    }


    public void AttackEndEvent()
    {
        isAttacking = false;
    }
}