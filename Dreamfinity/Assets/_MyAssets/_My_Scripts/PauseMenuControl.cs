using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using C_Utilities;

public class PauseMenuControl : MonoBehaviour {

    public Trigger isPaused;
    GameObject pauseMenu;
    Canvas menuCanvas;

    private void Awake()
    {
        menuCanvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        isPaused.Reset();
    }


    private void OnApplicationPause(bool pause)
    {
        

        if (!pause)
        {
            menuCanvas.enabled = false;
            //Debug.Log("Not Paused");
            Time.timeScale = 1;
        }
        
        if(pause)
        {
            menuCanvas.enabled = true;
            //Debug.Log("Paused");
            Time.timeScale = 0;
        }
    }


    public void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            isPaused.Flip();
        }
        
        OnApplicationPause(isPaused.state);
        
        
    }
}
