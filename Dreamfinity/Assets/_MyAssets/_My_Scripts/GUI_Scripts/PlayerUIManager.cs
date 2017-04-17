using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour {


    GameObject pLayerRef;
    LucidityControl playerLucidityRef;
    PlayerHP playerHpRef;
    Slider healthSliderRef;
    Slider luciditySliderRef;

    private void Awake()
    {
        pLayerRef = GameObject.FindWithTag("Player");
        playerLucidityRef = pLayerRef.GetComponent<LucidityControl>();
        luciditySliderRef = GameObject.FindWithTag("PlayerLucidityUI").GetComponent<Slider>();
        playerHpRef = pLayerRef.GetComponent<PlayerHP>();
        healthSliderRef = GameObject.FindWithTag("PlayerHealthUI").GetComponent<Slider>();
    }

    void Start()
    {
        luciditySliderRef.maxValue = playerLucidityRef.limit;
        healthSliderRef.maxValue = playerHpRef.maxHealth;
        healthSliderRef.minValue = luciditySliderRef.minValue = 0;
    }

    void Update () {

        luciditySliderRef.value = playerLucidityRef.balance;
        healthSliderRef.value = playerHpRef.currentHealth; 
    }
}
