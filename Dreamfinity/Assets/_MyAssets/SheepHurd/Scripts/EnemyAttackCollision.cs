using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour {

    SphereCollider colRef;
    public int damage = 5;


    public void Awake()
    {
        colRef = GetComponent<SphereCollider>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHP>().TakeDamage(damage);
            other.gameObject.GetComponent<Animator>().SetTrigger("Hit");
            colRef.enabled = false;
            
        }
    }
}
