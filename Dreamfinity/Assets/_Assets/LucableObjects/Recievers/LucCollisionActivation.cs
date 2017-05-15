using UnityEngine;
using C_Utilities;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]


public class LucCollisionActivation : MonoBehaviour {

    public Trigger activated;
    
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.transform.tag == "Casting")
        {
            activated.state = true;
            //GameObject.Destroy(other.collider.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        activated.Reset();
    }
}
