using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayChanger : MonoBehaviour {
    [SerializeField] Transform way = null;
    [SerializeField] Transform FirstWheel = null;
    [Range (0f, 10f)]
    [SerializeField] float MaxDeviation = 1;

    [SerializeField] float MaxTimeToChange = 15;
    [Range (0.01f, 100)]
    [SerializeField] float animSmooth = 1;
    bool NowChangeWay = false;
    float originalPositionX = 0;
    float endPositionX = 0;
    bool RightDirection = true;
    Rigidbody _rigidbody = null;
    void Start () {
        originalPositionX = way.position.x;
        _rigidbody = GetComponent<Rigidbody> ();
        ChangeWay ();
    }
    void ChangeWay () {
        endPositionX = originalPositionX + Random.Range (-MaxDeviation, MaxDeviation);
        if (endPositionX < originalPositionX)
            RightDirection = false;
        else {
            RightDirection = true;
        };
        //var rotation = 180 * (endPositionX - way.position.x) / MaxDeviation;
        // Debug.Log (rotation);
        // FirstWheel.Rotate (0, 0, rotation);
        //Debug.Log ("endPositionX " + endPositionX);
        NowChangeWay = true;
        Invoke ("ChangeWay", Random.Range (0, MaxTimeToChange));
    }
    private void Update () {
        if (!NowChangeWay)
            return;
        way.position += new Vector3 (endPositionX / animSmooth, way.position.y, way.position.z);
        if (RightDirection) {
            if (way.position.x > endPositionX)
                Chill ();
        } else if (!RightDirection && way.position.x < endPositionX)
            Chill ();
    }
    private void Chill () {
        NowChangeWay = false;
        //   FirstWheel.Rotate (0, 0, FirstWheel.rotation.z);

    }

}