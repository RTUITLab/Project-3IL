using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public PathCreator pathCreator;
    public float distance = 2;

    public bool right = false;
    public bool left = false;

    public float leftForce = 150;
    public float rightForce = 150;

    private void Start()
    {
    }

    private void Update()
    {
        if (left)
        {
            var leftRay = new Ray(GetLeftCenterPosition() - transform.up * 2, transform.up);
            Debug.DrawLine(leftRay.origin, leftRay.origin + leftRay.direction * 4, Color.red);
            if (Physics.Raycast(leftRay, out var leftHitInfo, 4, LayerMask.GetMask("Wheels")))
            {
                leftHitInfo.collider.gameObject.GetComponent<Wheel>()?.DoBarrier(leftForce);
                Debug.LogError("LEFT");
            }
        }
        if (right)
        {
            var rightRay = new Ray(GetRightCenterPosition() - transform.up * 2, transform.up);
            Debug.DrawLine(rightRay.origin, rightRay.origin + rightRay.direction * 4, Color.red);
            if (Physics.Raycast(rightRay, out var rightHitInfo, 4, LayerMask.GetMask("Wheels")))
            {
                rightHitInfo.collider.gameObject.GetComponent<Wheel>()?.DoBarrier(rightForce);
                Debug.LogError("RIGHT");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);

        var leftPosition = GetLeftCenterPosition();
        var rightPosition = GetRightCenterPosition();
        DrawGizmoWheel(left ? Color.green : Color.red, leftPosition);
        DrawGizmoWheel(right ? Color.green : Color.red, rightPosition);
    }

    private Vector3 GetRightCenterPosition()
    {
        return transform.position + transform.right * distance;
    }

    private Vector3 GetLeftCenterPosition()
    {
        return transform.position - transform.right * distance;
    }

    private void DrawGizmoWheel(Color color, Vector3 position)
    {

        Gizmos.color = color;
        Gizmos.DrawSphere(position, 0.9f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(position, 1);
    }
}
