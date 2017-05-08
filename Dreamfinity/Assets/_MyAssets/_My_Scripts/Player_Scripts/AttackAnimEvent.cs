using UnityEngine;
using System.Collections;

public class AttackAnimEvent: MonoBehaviour
{
    public bool isAttacking;
    public bool animationBegin;
    public bool startStep;

    public void AttackAnimationBegin()
    {
        animationBegin = true;
    }

    public void AttackStepBegin()
    {
        startStep = true;
    }

    public void AttackBeginEvent()
    {
        isAttacking = true;
    }

    public void AttackEndEvent()
    {
        isAttacking = false;
        animationBegin = false;



    }
}