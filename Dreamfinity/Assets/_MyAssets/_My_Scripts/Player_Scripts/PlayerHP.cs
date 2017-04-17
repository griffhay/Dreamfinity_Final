using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {

	public int startingHealth = 5;
	public int currentHealth;
	public int maxHealth = 5;
	public Slider healthSlider;
	bool isDead;
	bool damaged;
	// public Image damageImage;
	// public AudioClip deathClip;
	// public float flashSheepd = 5f;
	// Color flashColour = Color(1f, 0f, 0f, 0.1f);
	// Animator anim;
	// Audiosource playerAudo;


	// Use this for initialization
	void Start () {

		currentHealth = startingHealth;
		// anim = GetComponent <Animator> ();
		// playerAudio = GetComponent <AudioSource> ();
		healthSlider= GameObject.FindWithTag("PlayerHealthUI").GetComponent<Slider>();
		HealthUIUpdate ();
		
	}

	void HealthUIUpdate() 
	{
		healthSlider.minValue = 0;
	}
    
	// Update is called once per fra	me
	void Update () {

		if (damaged) {
			//damageImage.color = flashColour;
		}
		else
		{
			// damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
		healthSlider.maxValue = maxHealth;
		healthSlider.value = currentHealth;
		// maxHealth = maxHealth + 2;
		// currentHealth = currentHealth + 1;
		if (currentHealth > maxHealth) 
		{
			currentHealth = maxHealth;
		}
		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}


	public void TakeDamage(int amount)
	{
		damaged = true;

		currentHealth -= amount;

		healthSlider.value = currentHealth;
		//playerAudio.play();

		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}

		void Death()
		{
			//disable any anims/projectiles
			// anim.SetTrigger("Die") tell the animator you died
			// tell audio that you died
			int scene = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(scene,LoadSceneMode.Single);

		}



	}