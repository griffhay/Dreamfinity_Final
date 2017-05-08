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


    private void OnPause(bool pause)
    {
        

        if (!pause)
        {
            menuCanvas.enabled = false;
            Time.timeScale = 1;
        }
        
        if(pause)
        {
            menuCanvas.enabled = true;
            Time.timeScale = 0;
        }
    }


    public void Update()
    {
        OnPause(isPaused.state);  
    }
}
