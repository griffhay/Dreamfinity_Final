using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidityControl : MonoBehaviour {

    public int balance;  //The amount of the rescourse the parent Object has.
    public int limit;    //The maximum amount the parent object can have.
    DrainLucidityAct drain;    //A refferance to the action script to drain from a well;
    LucidityControl resCont; //Ref to this resourse Control
    private bool draining;    // True when the parent object is draining from a well;
    private bool casting;    // True when the parent onbect is casting the rescourse;
    GameObject lucProjectileRef;
    GameObject looseLucPoint;

    private void Start()
    {
        lucProjectileRef  = Resources.Load("pfb_RescourseCast") as GameObject;
    }

    //this function is called by parent objects to adjust the value of the rescourse incrementaly
    public void Deposit(int value)
    {
        if (value + balance <= limit)
        {
            balance += value;
        }
    }

    public void Withdraw(int value)
    {
        if (balance - value >= 0)
        {
            balance -= value;
        }
    }


    public void Transfer(LucidityControl to, int value)
    {
        if (value - balance >= 0)
        {
            balance -= value;
        }

        if (value + to.balance <= to.limit)
        {
            to.balance += value;
        }
    }

    public void TakeLuceDamage(int damageTaken)
    {
        Withdraw(damageTaken);
        for(int i = 0; i< damageTaken; i++)
        {
            GameObject lucDrop;
            lucDrop =  Instantiate(lucProjectileRef, transform.position , transform.rotation);
            lucDrop.GetComponent<LucidityProjectile>().OnTakeDamage(damageTaken, GetComponent<Collider>());
           
        }
    }
}