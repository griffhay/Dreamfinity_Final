﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using C_Utilities;

namespace Lucidity
{

    [RequireComponent(typeof(LucidityControl))]
    [RequireComponent(typeof(LucableObject))]
    [RequireComponent(typeof(Rigidbody))]

    public class PlatformMovementControl : MonoBehaviour
    {
        public Receiver receiver;
        Trigger recieverTigger;
        public bool activated;
        public GameObject recieverObj;

        PressurePlateControl pressPlateCont;
        LucCollisionActivation lucColActivation;
        RemoteCollisionReceiver remoteColCont;

        CharacterIKControl ikController;
        Rigidbody rigBodRef;
        public GameObject wayPointPrntObj;
        public GameObject[] wayPoints;
        public Vector3 posFrom, posTo;

        public int currentWayPointIndex, nextWayPointIndex;
        public float speed;
        public float lucBalanceF;

        float time;

        

        private void Start()
        {

            rigBodRef = GetComponent<Rigidbody>();
            activated = false;
            currentWayPointIndex = 0;
            nextWayPointIndex = 1;
            WayPointArrayAllocation();
            posFrom = wayPointPrntObj.transform.GetChild(currentWayPointIndex).transform.position;
            posTo = wayPointPrntObj.transform.GetChild(nextWayPointIndex).transform.position;

            switch (receiver)
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
        }

       


        private void OnCollisionEnter(Collision other)
        {       
            /* The activation function */
            if (other.collider.gameObject.transform.tag == "Casting" && !activated)
            {
                time = Time.time;
                posFrom = rigBodRef.position;
                activated = true;
                GameObject.Destroy(other.gameObject);
            }

            if(other.collider.gameObject.transform.tag == "Player")
            {
                other.collider.gameObject.transform.parent = this.transform;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.gameObject.transform.tag == "Player")
            {
                other.collider.gameObject.transform.parent = null;
            }
        }

        private void WayPointArrayAllocation()
        {
            if (wayPointPrntObj == null)
            {
                Debug.LogError(gameObject.name + " does not have a waypoint parent obj");
            }
            else       //Dynamcialy addjusting the array at the beging of the game;
            {
                wayPoints = new GameObject[wayPointPrntObj.transform.childCount];

                for (int i = 0; i < wayPointPrntObj.transform.childCount; i++)
                {
                    wayPoints[i] = wayPointPrntObj.transform.GetChild(i).gameObject;
                }
            }
        }

        public void FixedUpdate()
        {
            switch (receiver)
            {
                case Receiver.LucCollision:

                    if (lucColActivation.activated.state)
                    {
                        activated = true;
                        time = Time.fixedTime;
                    }

                    break;

                case Receiver.PressurePlate:

                    if (pressPlateCont.activated)
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

            if (activated)
            {
                TransPos();
            }

        }

        void WayPointIndexIncrease()
        {
            currentWayPointIndex++;
            if (currentWayPointIndex > wayPointPrntObj.transform.childCount - 1)
            {
                currentWayPointIndex = 0;
            }

            nextWayPointIndex++;
            if (nextWayPointIndex > wayPointPrntObj.transform.childCount - 1)
            {
                nextWayPointIndex = 0;
            }
        }

        

        void TransPos()
        {
            float distance = Vector3.Distance(rigBodRef.position, posTo);
            
            transform.position = Vector3.Lerp(posFrom ,posTo, Time.fixedTime - time);
            if(distance < 0.1f)
            {
                activated = false; 
                posFrom = transform.position;
                WayPointIndexIncrease();
                posTo = wayPointPrntObj.transform.GetChild(nextWayPointIndex).transform.position;
                
            }


            
        }
    }
}

//Vector3.Lerp(rigBodRef.position, posTo, Mathf.Pow(Mathf.Sin(Time.time * speed), 2))
