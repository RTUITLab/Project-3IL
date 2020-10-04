using UnityEngine;

public class TeleportOnGround : MonoBehaviour {
	[SerializeField] GameObject Way;
	[SerializeField] float height = 1;
	[SerializeField][Range (1, 5)] float animSpeed = 1;

	void Update () {
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast (transform.position, -Vector3.up, out hit)) {
			var distanceToGround = hit.distance;
			print ($"TeleportOnGround -> distanceToGround : {distanceToGround}");
			if (distanceToGround > height + 0.1)
				Way.transform.position = new Vector3 (Way.transform.position.x,
					//  Model.transform.position.y - (distanceToGround - height)
					Way.transform.position.y - 0.05f * animSpeed,
					Way.transform.position.z);
			else if (distanceToGround < height - 0.1)
				Way.transform.position = new Vector3 (Way.transform.position.x,
					Way.transform.position.y + 0.05f * animSpeed,
					Way.transform.position.z);
		}
	}
}