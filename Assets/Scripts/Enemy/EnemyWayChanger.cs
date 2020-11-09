using DG.Tweening;
using UnityEngine;
#region enums
enum WheelChangeRotate
{
    xRotate,
    yRotate,
    zRotate
}

enum WayDirection
{
    x,
    y,
    z
}
#endregion
public class EnemyWayChanger : MonoBehaviour
{
    public Transform way = null;
    [SerializeField] Transform[] wheels = new Transform[2];
    [SerializeField] [Range(0f, 10f)] float MaxDeviation = 5;
    [SerializeField] [Range(0f, 45f)] float MaxWheelRotate = 45;
    [SerializeField] float MaxTimeToChange = 7;
    [SerializeField] [Range(1, 3f)] float MovementSmooth = 0.5f;
    [SerializeField] WheelChangeRotate WheelChangeRotate = WheelChangeRotate.xRotate;
    [SerializeField] WayDirection WayDirection = WayDirection.x;
    bool NowChangeWay = false;
    float originalPosition = 0;
    float endPosition = 0;
    bool RightDirection = true;
    float distance;
    Rigidbody _rigidbody = null;
    void Start()
    {
        originalPosition = WayTranformCoord();
        _rigidbody = GetComponent<Rigidbody>();
        ChangeWay();
    }
    void ChangeWay()
    {
        // we simulate movement to the right or to the left
        endPosition = originalPosition + Random.Range(-MaxDeviation, MaxDeviation);
        RightDirection = endPosition < originalPosition ? false : true;
        distance = (endPosition - WayTranformCoord()) / MaxDeviation;
        // we multiply by the sign of the body rotation, because otherwise the wheel turns incorrectly
        WheelRotate((transform.eulerAngles.y < 180 ? -1 : 1) * MaxWheelRotate * distance, 0.5f);
        // print ($"43. EnemyWayChanger -> transform.rotation : {transform.eulerAngles}");
        distance = Mathf.Abs(distance);
        NowChangeWay = true;
        Invoke("ChangeWay", Random.Range(0, MaxTimeToChange));
    }

    float WayTranformCoord()
    {
        // we get the current coordinate: x, y or z
        if (WayDirection == WayDirection.x)
            return way.position.x;
        else if (WayDirection == WayDirection.y)
            return way.position.y;
        else
            return way.position.z;
    }
    private void Update()
    {
        if (!NowChangeWay)
            return;
        Vector3 temp;
        if (WayDirection == WayDirection.x)
            temp = new Vector3(endPosition, way.position.y, way.position.z);
        else if (WayDirection == WayDirection.y)
            temp = new Vector3(way.position.x, endPosition, way.position.z);
        else
            temp = new Vector3(way.position.x, way.position.y, endPosition);
        // we add 1 to the distance so the animation speed ONLY increases
        way.DOMove(temp, MovementSmooth * (distance + 1));
        double wayPosition = WayTranformCoord();
        // At this moment we do not change position anymore
        if ((RightDirection && WayTranformCoord() > endPosition) ||
            (!RightDirection && WayTranformCoord() < endPosition))
        {
            NowChangeWay = false;
            WheelRotate(0, 0.5f);
        }
    }
    private void WheelRotate(float Rotate, float animSpeed)
    {
        // Turn the wheel depending on the selected coordinate
        switch (WheelChangeRotate)
        {
            case WheelChangeRotate.xRotate:
                foreach (var wheel in wheels)
                    wheel.DOLocalRotate(new Vector3(Rotate, 0, 0), animSpeed);
                break;
            case WheelChangeRotate.yRotate:
                foreach (var wheel in wheels)
                    wheel.DOLocalRotate(new Vector3(0, Rotate, 0), animSpeed);
                break;
            case WheelChangeRotate.zRotate:
                foreach (var wheel in wheels)
                    wheel.DOLocalRotate(new Vector3(0, 0, Rotate), animSpeed);
                break;
        }
    }
}