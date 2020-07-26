using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWheelScript : MonoBehaviour
{
    public float force = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * force);
        }
    }
}
