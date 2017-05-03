using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]

public class loadController : MonoBehaviour {

    float timer;
    VideoPlayer video;

	void Start () {
        timer = Time.fixedTime;
        video = GameObject.FindWithTag("MainCamera").GetComponent<VideoPlayer>();

    AudioSource audio = GameObject.FindWithTag("audio_load").GetComponent<AudioSource>();
    audio.Play();

	}
    	
	void FixedUpdate () {
        Debug.Log(Time.fixedTime - timer);

        if (Time.fixedTime - timer >= 7.11f)
        {
            SceneManager.LoadScene("World_beta_Final");
            Debug.Log("wtb shower");
        }

    }
}
