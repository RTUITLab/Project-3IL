using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    Transform local;
    Rigidbody RB;

    private void Start()
    {
        local = gameObject.transform;
        RB = GetComponent<Rigidbody>();
    }
    void Update()
    {
        local.transform.localPosition = new Vector3(0, local.localPosition.y, 0);
    }

    void FixedUpdate()
    {
        Vector3 oldRot = transform.rotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(oldRot.x, 0, oldRot.z);
    }
}
