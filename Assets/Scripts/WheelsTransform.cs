using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsTransform : MonoBehaviour
{
    [SerializeField] Transform WheelFL_T;
    [SerializeField] Transform WheelFR_T;
    [SerializeField] Transform WheelRL_T;
    [SerializeField] Transform WheelRR_T;

    [SerializeField] WheelCollider WheelFL_C;
    [SerializeField] WheelCollider WheelFR_C;
    [SerializeField] WheelCollider WheelRL_C;
    [SerializeField] WheelCollider WheelRR_C;

    void FixedUpdate()
    {
        updWheelPos(WheelFL_T, WheelFL_C);
        updWheelPos(WheelFR_T, WheelFR_C);
        updWheelPos(WheelRL_T, WheelRL_C);
        updWheelPos(WheelRR_T, WheelRR_C);
    }

    void updWheelPos(Transform wheel, WheelCollider collider)
    {
        Vector3 pos = wheel.position;
        Quaternion rot = wheel.rotation;

        collider.GetWorldPose(out pos, out rot);

        wheel.position = pos;
        //wheel.rotation = rot;
    }
}
