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

    void Awake()
    {
        pauseCanvas = GameObject.Find("pauseCanvas").gameObject.GetComponent<Canvas>();
        quitCanvas = GameObject.Find("quitCanvas").gameObject.GetComponent<Canvas>();
    }

    void Start()
    {
        pauseCanvas.enabled = true;
        quitCanvas.enabled = false;

    }

    void Update()
    {

    }

    void OnPressPauseResume()
    {
        Debug.Log("caesar me");
        pauseCanvas.enabled = false;
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
        Application.Quit();

        AudioSource audio = GameObject.FindWithTag("audio_select").GetComponent<AudioSource>();
        audio.Play();

    }

}
