using System.Collections;
using System.Reflection;
using DG.Tweening;
using UnityEngine;

public class EnemyWayChanger : MonoBehaviour {
    public Transform way = null;
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
    float distance;
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
        distance = (endPositionX - way.position.x) / MaxDeviation;
        var rotation = 30 * distance;
        if (distance < 0)
            distance = -distance;
        FirstWheel.DOLocalRotate (new Vector3 (0.0f, 0.0f, rotation), 0.5f);
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
        FirstWheel.DOLocalRotate (new Vector3 (0.0f, 0.0f, 0.0f), 0.5f);
    }

}