using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsTransform : MonoBehaviour
{
    [SerializeField] Transform[] Wheels_T;
    [SerializeField] WheelCollider[] Wheels_C;

    void FixedUpdate()
    {
        for (int i = 0; i < Wheels_T.Length - 1; i++)
        {
            if (Wheels_T[i] != null)
            {
                updWheelPos(Wheels_T[i], Wheels_C[i]);
            }
        }
    }

    void updWheelPos(Transform wheel, WheelCollider collider)
    {
        Vector3 pos = wheel.position;
        Quaternion rot = wheel.rotation;

        collider.GetWorldPose(out pos, out rot);

        wheel.position = pos;
    }
}
