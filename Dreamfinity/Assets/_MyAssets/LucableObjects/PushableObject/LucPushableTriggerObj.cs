using UnityEngine;
using System.Collections;
using C_Utilities;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]


public class LucPushableTriggerObj : MonoBehaviour
{
    GameObject parentobject;
    Rigidbody parentRigBod;

    public Trigger activated;
    public float pushMultiplier;
    float time;

    public Vector3 targetDirection;

    private void Awake()
    {
        parentobject = transform.parent.gameObject;
        parentRigBod = parentobject.GetComponent<Rigidbody>();
    }


    // Use this for initialization
    void Start()
    {
        RigidbodyInit();
        MeshColliderInit();
    }

    private void MeshColliderInit()
    {
        MeshCollider colliderRef = GetComponent<MeshCollider>();
        colliderRef.isTrigger = true;
        colliderRef.convex = true;
    }

    private void RigidbodyInit()
    {
        Rigidbody rigBodRef = GetComponent<Rigidbody>();
        rigBodRef.useGravity = false;
        rigBodRef.isKinematic = true;
        rigBodRef.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == ("Casting"))
        {
            Destroy(other.gameObject);
            targetDirection = parentRigBod.transform.position + transform.forward * pushMultiplier;
            time = Time.time;
            activated.Trip();
        }
    }

    private void Transpose()
    {
        float timer = Time.time - time;

        parentRigBod.MovePosition(Vector3.Slerp(parentRigBod.transform.position,targetDirection, timer));

        if(timer >= 1)
        {
            activated.Reset();
            
        }

    }

    void FixedUpdate()
    {
        if(activated.state == true)
        {
            Transpose();
        }
    }
}















































