using System.Collections;
using PathCreation;
using UnityEngine;
// Moves along a path at constant speed.
// Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
public class PathFollower : MonoBehaviour {
    public string PathName = "ZILpath";
    public PathCreator pathCreator;
    [SerializeField] EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;
    public bool needRotate;
    public bool findPathName = true;
    private void Awake () {
        if (findPathName)
            pathCreator = GameObject.Find (PathName).GetComponent<PathCreator> ();
    }

    private void start () {
        if (pathCreator != null)
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
    }
    void Update () {
        if (pathCreator != null) {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance (distanceTravelled, endOfPathInstruction);
            if (needRotate) {
                var euler = pathCreator.path.GetRotationAtDistance (distanceTravelled, endOfPathInstruction).eulerAngles;
                euler = new Vector3 (euler.x, euler.y, euler.z + 90);
                transform.rotation = Quaternion.Euler (euler);
            } else {
                transform.rotation = pathCreator.path.GetRotationAtDistance (distanceTravelled, endOfPathInstruction);
            }
        }
    }
    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged () => distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath (transform.position);
}