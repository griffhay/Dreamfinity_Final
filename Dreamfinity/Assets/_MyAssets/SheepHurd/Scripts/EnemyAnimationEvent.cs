using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour {

    public bool grabSheep, throwSheep;

    void GrabSheepAnimEvent()
    {
        grabSheep = true;
        Debug.Log("GrabedSheep");
    }

    void ThrowSheepAnimEvent()
    {
        throwSheep = true;
        Debug.Log("ThrowedSheep");
    }
    void AttackBegin()
    {
        GetComponent<EnemyController>().hitCollider.enabled = true;
    }
    void AttackOver()
    {
        GetComponent<EnemyController>().hitCollider.enabled = false;
    }

}
