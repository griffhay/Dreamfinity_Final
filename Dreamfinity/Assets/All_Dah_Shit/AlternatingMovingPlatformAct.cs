using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C_Utilities;

namespace Lucidity
{

    [RequireComponent(typeof(LucidityControl))]
    [RequireComponent(typeof(LucableObject))]
    [RequireComponent(typeof(Rigidbody))]

    public class AlternatingMovingPlatformAct : MonoBehaviour
    {
        Trigger recieverTigger;
        public bool activated;
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

            if (other.collider.gameObject.transform.tag == "Player")
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
            TransPos();
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

            transform.position = Vector3.Lerp(posFrom, posTo, (Time.fixedTime - time)/speed);
            if (distance < 0.1f)
            {
                posFrom = transform.position;
                time = Time.time;
                WayPointIndexIncrease();
                posTo = wayPointPrntObj.transform.GetChild(nextWayPointIndex).transform.position;

            }
        }
    }
}

//Vector3.Lerp(rigBodRef.position, posTo, Mathf.Pow(Mathf.Sin(Time.time * speed), 2))



