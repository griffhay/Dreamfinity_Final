using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lucidity;
using C_Utilities;




public class LucRotatorParent : MonoBehaviour {
	[Header("Lucidity Options")]
	public Receiver receiver;
	Trigger recieverTigger;
	public bool  activated;
	public GameObject recieverObj;

	[Header("Rotation Options")]
	public int rotationSegments;

	PressurePlateControl pressPlateCont;
	LucCollisionActivation lucColActivation;
	RemoteCollisionReceiver remoteColCont;

	Vector3[] rotations;
	float rotEulerVal;
	float time;
	int counter;

	// Use this for initialization
	void Start()
	{
		switch(receiver)
		{
		case Receiver.LucCollision:  
			lucColActivation = GetComponent<LucCollisionActivation>();
			break;

		case Receiver.PressurePlate:

			if (recieverObj.transform.tag != "PresurePlate")
			{
				Debug.Log("(" + gameObject.name + "'s ) reciver object is not a pressure plate or does not exist \r Please assigne a pressure plate object in the inspector.");
			}
			else
			{
				pressPlateCont = recieverObj.GetComponent<PressurePlateControl>();
			}

			break;

		case Receiver.RemoteCollision:
			if (recieverObj.transform.tag != "RemoteCollision")
			{
				Debug.Log("(" + gameObject.name + "'s ) reciver object is not a Remote Collision Object or does not exist \r Please assigne a pressure plate object in the inspector.");
			}
			else
			{
				remoteColCont = recieverObj.GetComponent<RemoteCollisionReceiver>();
			}
			break;

		}

		rotEulerVal = 360 / rotationSegments;
		rotations = new Vector3[rotationSegments];

		for (int i = 1; i <= rotationSegments; i++)
		{
			rotations[i - 1] = new Vector3(0, (360 / rotationSegments) * (i), 0);
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		switch(receiver)
		{
		case Receiver.LucCollision:

			if(lucColActivation.activated.state)
			{
				activated = true;
				time = Time.fixedTime;
			}

			break;

		case Receiver.PressurePlate:

			if(pressPlateCont.activated)
			{
				activated = true;
				time = Time.fixedTime;
			}
			break;

		case Receiver.RemoteCollision:

			if (remoteColCont.activated.state)
			{
				activated = true;
				time = Time.fixedTime;
			}
			break;
		}

		Transrotate(activated);
	}

	private void Transrotate(bool _activated)
	{
		if (_activated)
		{
			float timer = Time.fixedTime - time;
			Quaternion nextRot = Quaternion.Euler(rotations[counter]);
			transform.rotation = Quaternion.Slerp(transform.rotation, nextRot, timer);

			if (timer >= 1)
			{
				activated = false;

				counter++;
				if (counter >= rotationSegments)
				{
					counter = 0;
				}
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.gameObject.transform.tag == "Player")
		{
			other.collider.gameObject.transform.parent = this.transform;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.collider.gameObject.transform.tag == "Player")
		{
			other.collider.gameObject.transform.parent = null;
		}
	}
}

