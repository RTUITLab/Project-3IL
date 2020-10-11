using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class GroundSyncForZIl : MonoBehaviour {

    [SerializeField] GameObject way;
    [SerializeField] float height = 1;

    void FixedUpdate () {
        RaycastHit hit = new RaycastHit ();
        if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
            var distanceToGround = hit.distance;
            print ($"12. TeleportOnGround -> distanceToGround : {distanceToGround}");
            way.transform.DOMoveY (way.transform.position.y + height - 0.05f, 0);
        } else
            way.transform.DOMoveY (way.transform.position.y + 0.05f, 0);
    }
}