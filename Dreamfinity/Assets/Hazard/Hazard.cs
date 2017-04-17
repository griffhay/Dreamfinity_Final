using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    public float timer = 0;
    public float tickRate = 2;
    public int initialDamage;
    public int sustainDamage;
    PlayerHP playerHPref;

    private void Start()
    {
        playerHPref = GameObject.FindWithTag("Player").GetComponent<PlayerHP>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            playerHPref.TakeDamage(initialDamage);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            if (timer >= tickRate)
            {
                timer -= tickRate;

                playerHPref.TakeDamage(sustainDamage);
            }
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            timer = 0;
        }
    }

}
