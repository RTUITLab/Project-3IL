using UnityEngine;

public class TeleportOnGround : MonoBehaviour {
	[SerializeField] GameObject way;
	[SerializeField] float height = 1;
	[SerializeField][Range (1, 5)] float animSpeed = 1;
	void Update () {
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast (transform.position, -Vector3.up, out hit)) {
			var distanceToGround = hit.distance;
			if (distanceToGround > height + 0.1)
				way.transform.position = new Vector3 (way.transform.position.x,
					way.transform.position.y - 0.05f * animSpeed,
					way.transform.position.z);
			else if (distanceToGround < height - 0.1)
				way.transform.position = new Vector3 (way.transform.position.x,
					way.transform.position.y + 0.05f * animSpeed,
					way.transform.position.z);

		}
	}
}