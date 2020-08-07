using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{

    private Rigidbody editablerigidBody;
    public Vector3 centerOfMass;

    void Start()
    {
        editablerigidBody = GetComponent<Rigidbody>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * centerOfMass, 1);
    }
    // Update is called once per frame
    void Update()
    {
        editablerigidBody.centerOfMass = centerOfMass;
    }
}
