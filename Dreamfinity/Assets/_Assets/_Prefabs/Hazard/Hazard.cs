using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour {

    public float timer = 0;
    public float tickRate = 2;
    public int initialDamage;
    public int sustainDamage;
    LucidityControl PlayerLucidityRef;

    private void Start()
    {
        PlayerLucidityRef = GameObject.FindWithTag("Player").GetComponent<LucidityControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            PlayerLucidityRef.TakeLuceDamage(initialDamage);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            if (timer >= tickRate)
            {
                timer -= tickRate;

                PlayerLucidityRef.TakeLuceDamage(sustainDamage);
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
