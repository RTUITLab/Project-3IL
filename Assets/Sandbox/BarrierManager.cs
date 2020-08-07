using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BarrierManager : MonoBehaviour
{
    (Barrier, float)[] barriers;
    public PathFollower pathFollower;
    void Start()
    {
        //barriers = GetComponentsInChildren<Barrier>()
        //    .Where(b => b.pathCreator == pathFollower.pathCreator)
        //    .Select(b => (barrier: b, distance: b.pathCreator.path.GetClosestDistanceAlongPath(b.transform.position)))
        //    .OrderBy(p => p.distance)
        //    .ToArray();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
