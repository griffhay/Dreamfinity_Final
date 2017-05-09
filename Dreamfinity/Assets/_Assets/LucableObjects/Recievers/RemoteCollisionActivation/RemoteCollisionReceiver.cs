using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C_Utilities;

public class RemoteCollisionReceiver : MonoBehaviour {

    public Trigger activated;

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.transform.tag == "Casting")
        {
            activated.state = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        activated.Reset();
    }
}
