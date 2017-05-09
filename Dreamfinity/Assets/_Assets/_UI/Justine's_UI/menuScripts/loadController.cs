using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]

public class loadController : MonoBehaviour {

    public float timer;
    VideoPlayer video;

	void Start () {
        timer = Time.time;
        video = GameObject.FindWithTag("MainCamera").GetComponent<VideoPlayer>();
        AudioSource audio = GameObject.FindWithTag("audio_load").GetComponent<AudioSource>();
        audio.Play();

	}


    	
	void Update () {
        Debug.Log(Time.time - timer);

        if (Time.time - timer >= 7.11f)
        {
            
            SceneManager.LoadScene(2);
        }

    }
}
