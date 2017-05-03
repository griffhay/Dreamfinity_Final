using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C_Utilities;



namespace Lucidity
{

    public enum InterpolationType { cycle, alternate }
    [RequireComponent(typeof(LucableObject))]
    [RequireComponent(typeof(LucCollisionActivation))]
    [RequireComponent(typeof(AudioSource))]
    public class LucScale : MonoBehaviour {
        public Receiver receiver;
        Trigger recieverTigger;
        public bool activated;
        public GameObject recieverObj;

       
        Vector3[] size;
        [Range(.01f, 10f)]
        public float sizeA,sizeB, sizeC;
        [Range(0,2)]
        public int i;//Used to cycle through the size array
        float time;
        int alternate;

        PressurePlateControl pressPlateCont;
        LucCollisionActivation lucColActivation;
        RemoteCollisionReceiver remoteColCont;

        private void Start()
        {
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

            size = new Vector3[3];
            size[0] = new Vector3(sizeA, sizeA, sizeA);
            size[1] = new Vector3(sizeB, sizeB, sizeB);
            size[2] = new Vector3(sizeC, sizeC, sizeC);
            alternate = 1;
            activated = false;
        }

        private void FixedUpdate()
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
                TransScale();
            }
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if(collision.gameObject.transform.tag == "Casting")
        //    {
        //        time = Time.time;
        //        activated = true;
        //    }
        //}

        private void TransScale()
        {
            //play audio for scale
             AudioSource audio = GameObject.FindWithTag("AudioPop").GetComponent<AudioSource>();
             audio.Play();
            transform.localScale = Vector3.Lerp(transform.lossyScale, size[i], Time.fixedTime - time);
            if(transform.lossyScale == size[i])
            {
                activated = false;
                if(i < 2)
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
            }
        }

        int IntCounter(int counter, InterpolationType interpolateType, int range)
        {
            switch (interpolateType)
            {
                case InterpolationType.alternate:

                    break;
                case InterpolationType.cycle:
                    break;
            }

            return counter;
        }
    }
}
