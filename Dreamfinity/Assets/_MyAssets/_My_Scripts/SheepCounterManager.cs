using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SheepCounterManager : MonoBehaviour {

    Text sheepCounterText;
    public int sheepCount;
    string outPutText;
    private void Awake()
    {
        sheepCounterText = GameObject.FindWithTag("SheepCounterText").GetComponent<Text>();
    }


    private void Update()
    {
        outPutText = sheepCount.ToString();
        sheepCounterText.text = outPutText;
    }


}
