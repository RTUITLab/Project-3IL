using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    Transform local;
    public bool FreezeRotationX = false;
    public bool FreezeRotationY = false;
    public bool FreezeRotationZ = false;

    public bool FreezePositionXZ = false;
    public bool FreezePositionYZ = false;
    public bool FreezePositionYX = false;

    public bool needToFix = false;

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
        if (FreezeRotationX)
        {
            Vector3 oldRot = local.localEulerAngles;
            transform.localRotation = Quaternion.Euler(0, oldRot.y, oldRot.z);
        }
        else if (FreezeRotationY)
        {
            Vector3 oldRot = local.localEulerAngles;
            transform.localRotation = Quaternion.Euler(oldRot.x, 0, oldRot.z);
        }
        else if (FreezeRotationZ)
        {
            Vector3 oldRot = local.localEulerAngles;
            transform.localRotation = Quaternion.Euler(oldRot.x, oldRot.y, 0);
        }
        if (needToFix)
        {
            if ((local.eulerAngles.z < 320f && local.eulerAngles.z > 30f) || (local.eulerAngles.z < 330f && local.eulerAngles.z > 320f))
            {
                Vector3 oldRot = local.localEulerAngles;
                transform.localRotation = Quaternion.Euler(oldRot.x, oldRot.y, 0);
            }
        }
    }

    void SyncPos()
    {
        if (FreezePositionXZ)
        {
            local.localPosition = new Vector3(0, local.localPosition.y, 0);
        }
        else if (FreezePositionYZ)
        {
            local.localPosition = new Vector3(local.localPosition.x, 0, 0);
        }
        else if (FreezePositionYX)
        {
            local.localPosition = new Vector3(0, 0, local.localPosition.z);
        }
    }
}
