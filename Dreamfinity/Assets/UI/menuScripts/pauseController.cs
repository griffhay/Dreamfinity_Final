using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class pauseController : MonoBehaviour {

    Canvas pauseCanvas;
    Canvas quitCanvas;
    Button resume_txt;
    Button quit_txt;
    Button yes_txt;
    Button no_txt;
    bool isPaused;

    void Awake()
    {
        pauseCanvas = GameObject.FindWithTag("pauseCanvas").gameObject.GetComponent<Canvas>();
        quitCanvas = GameObject.FindWithTag("quitCanvas").gameObject.GetComponent<Canvas>();
    }

    void Start()
    {
        //pauseCanvas.enabled = true;
        quitCanvas.enabled = false;
        pauseCanvas.enabled = false;
        isPaused = false;

    }

    void Update()
    { 
        if (Input.GetButtonDown("Pause"))
        {
            if (!isPaused)
            {
                isPaused = true;
                pauseCanvas.enabled = true;

            }

            else
            {
                isPaused = false;
                pauseCanvas.enabled = false;
            }
        }

        OnPause(isPaused);
    }

    void OnPause(bool _isPaused)
    {
        if (!_isPaused)
        {
            
            //Debug.Log("Not Paused");
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (_isPaused)
        {
            //Debug.Log("Paused");
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnPressPauseResume()
    {

        isPaused = false;
        pauseCanvas.enabled = false;
        Debug.Log("caesar me");
        //pauseCanvas.enabled = false;
        //SceneManager.LoadScene("World_beta_Final");

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressPauseQuit()
    {
        Debug.Log("ranch me");
        pauseCanvas.enabled = false;
        quitCanvas.enabled = true;

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressPauseNo()
    {
        Debug.Log("balsamic me");
        quitCanvas.enabled = false;
        pauseCanvas.enabled = true;

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

    void OnPressPauseYes()
    {
        Debug.Log("oil & vinegar me");
        quitCanvas.enabled = false;
        SceneManager.LoadScene(0);
        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

}
