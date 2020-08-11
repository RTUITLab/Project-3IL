using System.Collections;
using System.Reflection;
using DG.Tweening;
using UnityEngine;
enum ChangeRotate {
    xRotate,
    yRotate,
    zRotate
}
public class EnemyWayChanger : MonoBehaviour {
    public Transform way = null;

    [SerializeField] Transform[] wheels = new Transform[2];
    [Range (0f, 10f)]
    [SerializeField] float MaxDeviation = 5;
    [Range (0f, 45f)]
    [SerializeField] float MaxWheelRotate = 45;
    [SerializeField] float MaxTimeToChange = 7;
    [Range (0.01f, 100)]
    [SerializeField] float animSmooth = 40;
    [SerializeField] ChangeRotate changeRotate = ChangeRotate.xRotate;
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
        var rotation = MaxWheelRotate * distance;
        if (distance < 0)
            distance = -distance;
        WheelRotate (rotation, 0.5f);
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
        WheelRotate (0, 0.5f);
    }
    private void WheelRotate (float Rotate, float animSpeed) {
        switch (changeRotate) {
            case ChangeRotate.xRotate:
                {
                    for (int i = 0; i < wheels.Length; i++)
                        wheels[i].DOLocalRotate (new Vector3 (Rotate, 0, 0), animSpeed);
                }
                break;
            case ChangeRotate.yRotate:
                {
                    for (int i = 0; i < wheels.Length; i++)
                        wheels[i].DOLocalRotate (new Vector3 (0, Rotate, 0), animSpeed);
                }
                break;
            case ChangeRotate.zRotate:
                {
                    for (int i = 0; i < wheels.Length; i++)
                        wheels[i].DOLocalRotate (new Vector3 (0, 0, Rotate), animSpeed);
                }
                break;
        }
    }
}