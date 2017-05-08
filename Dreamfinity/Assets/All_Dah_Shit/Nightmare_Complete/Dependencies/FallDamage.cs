using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour {


    CharacterControler charControlRef;
    public float currentY;
    public float maxY;
    public float endY;
    public float yDistance;
    public bool air;
    PlayerHP playerHPRef;
    public int totalDamage;

    // add in a multiplier later that tracks Y velocity and uses raycast stuff and physics nerd stuff, rigidbody raycasting physics to calculate a multiplier on velocity


	// Use this for initialization
	void Start () {
        charControlRef = GetComponent<CharacterControler>();
        playerHPRef = GetComponent<PlayerHP>();

	}
	
	// Update is called once per frame
	void Update () {

        if (charControlRef.m_isGrounded == true)
        {
            if (air == true)
            {
                yDistance = maxY - endY;
                TakeFallDamage(yDistance);
                playerHPRef.TakeDamage(totalDamage);
                air = false;
            }
            if (air == false)
            {
                maxY = currentY;
            }
        }

        if (charControlRef.m_isGrounded == false)
        {
            currentY = transform.position.y;
            if (currentY > maxY)
            {
                maxY = currentY;
            }

        }
	}
    private void LateUpdate()
    {
        if (charControlRef.m_isGrounded == false)
        {
            endY = transform.position.y;
            air = true;
        }
        if (charControlRef.m_isGrounded == true)
        {
            yDistance = maxY - endY;
        }
    }

    public void TakeFallDamage(float damageValue)
    {
        if (damageValue > 10 && damageValue < 25)
        {
            totalDamage = Mathf.FloorToInt(damageValue);
            totalDamage = totalDamage / 2;
            Debug.Log("falldamage1: " + totalDamage);
        }
        else if (damageValue >= 25 && damageValue < 100)
        {
            totalDamage = Mathf.FloorToInt(damageValue);
            totalDamage = (((totalDamage - 25)/5)+15);
            Debug.Log("falldamage2: " + totalDamage);
        }
        else if (damageValue >= 100)
        {
            totalDamage = 0;
        }
    }
}
