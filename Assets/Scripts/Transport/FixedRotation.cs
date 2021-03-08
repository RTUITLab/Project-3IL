using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotation : MonoBehaviour
{
    Transform local;
    [SerializeField] bool FreezeRotationX = false;
    [SerializeField] bool FreezeRotationY = false;
    [SerializeField] bool FreezeRotationZ = false;

    [SerializeField] bool FreezePositionXZ = false;
    [SerializeField] bool FreezePositionYZ = false;
    [SerializeField] bool FreezePositionYX = false;

    [SerializeField] bool needToFix = false;
    float _angle = 30f;

    private void Start()
    {
        local = gameObject.transform;
        if (GameManager.instance.selectedMap == 3)
        {
            _angle = 70f;
        }
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
            if ((local.eulerAngles.y < 350f - _angle && local.eulerAngles.y > _angle) || (local.eulerAngles.y < 360f - _angle && local.eulerAngles.y > 350f - _angle))
            {
                oldRot = local.localEulerAngles;
                transform.localRotation = Quaternion.Euler(oldRot.x, 0, oldRot.z);
            }
            if ((local.eulerAngles.z < 350f - _angle && local.eulerAngles.z > _angle) || (local.eulerAngles.z < 360f - _angle && local.eulerAngles.z > 350f - _angle))
            {
                oldRot = local.localEulerAngles;
                transform.localRotation = Quaternion.Euler(oldRot.x, oldRot.y, 0);
            }
        }
        else if (FreezeRotationY)
        {
            Vector3 oldRot = local.localEulerAngles;
            transform.localRotation = Quaternion.Euler(oldRot.x, 0, oldRot.z);
            if ((local.eulerAngles.z < 350f - _angle && local.eulerAngles.z > _angle) || (local.eulerAngles.z < 360f - _angle && local.eulerAngles.z > 350f - _angle))
            {
                oldRot = local.localEulerAngles;
                transform.localRotation = Quaternion.Euler(oldRot.x, oldRot.y, 0);
            }
            if ((local.eulerAngles.x < 350f - _angle && local.eulerAngles.x > _angle) || (local.eulerAngles.x < 360f - _angle && local.eulerAngles.x > 350f - _angle))
            {
                oldRot = local.localEulerAngles;
                transform.localRotation = Quaternion.Euler(0, oldRot.y, oldRot.z);
            }
        }
        else if (FreezeRotationZ)
        {
            Vector3 oldRot = local.localEulerAngles;
            transform.localRotation = Quaternion.Euler(oldRot.x, oldRot.y, 0);
            if ((local.eulerAngles.y < 350f - _angle && local.eulerAngles.y > _angle) || (local.eulerAngles.y < 360f - _angle && local.eulerAngles.y > 350f - _angle))
            {
                oldRot = local.localEulerAngles;
                transform.localRotation = Quaternion.Euler(oldRot.x, 0, oldRot.z);
            }
            if ((local.eulerAngles.x < 350f - _angle && local.eulerAngles.x > _angle) || (local.eulerAngles.x < 360f - _angle && local.eulerAngles.x > 350f - _angle))
            {
                oldRot = local.localEulerAngles;
                transform.localRotation = Quaternion.Euler(0, oldRot.y, oldRot.z);
            }
        }
        if (needToFix)
        {
            if ((local.eulerAngles.z < 350f - _angle && local.eulerAngles.z > _angle) || (local.eulerAngles.z < 360f - _angle && local.eulerAngles.z > 350f - _angle))
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
