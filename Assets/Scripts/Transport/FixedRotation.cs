using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    Transform local;

    private void Start()
    {
        local = gameObject.transform;
    }
    private void Update()
    {
        SyncPos();
    }
    void FixedUpdate()
    {
        SyncRot();
    }

    void SyncRot()
    {
        Vector3 oldRot = local.localEulerAngles;
        transform.localRotation = Quaternion.Euler(oldRot.x, 0, oldRot.z);
    }

    void SyncPos()
    {
        local.localPosition = new Vector3(0, local.localPosition.y, 0);
    }
}
