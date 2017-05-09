using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour {


    GameObject pLayerRef;
    LucidityControl playerLucidityRef;


    Slider luciditySliderRef;

    private void Awake()
    {
        pLayerRef = GameObject.FindWithTag("Player");
        playerLucidityRef = pLayerRef.GetComponent<LucidityControl>();
        luciditySliderRef = GameObject.FindWithTag("PlayerLucidityUI").GetComponent<Slider>();
    }

    void Start()
    {
        luciditySliderRef.maxValue = playerLucidityRef.limit;
       
    }

    void Update () {

        luciditySliderRef.value = playerLucidityRef.balance;
        
    }
}
