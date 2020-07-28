using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Wheel : MonoBehaviour
{
    private Rigidbody rigidBody;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    public void DoBarrier(float force)
    {
        Debug.Log(name);
        rigidBody.AddRelativeForce(Vector3.up * force);
    }
}
