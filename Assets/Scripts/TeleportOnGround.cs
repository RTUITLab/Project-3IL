using DG.Tweening;
using UnityEngine;
public class TeleportOnGround : MonoBehaviour {
	[SerializeField] GameObject model;
	[SerializeField] float height = 1;
	void FixedUpdate () {
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
			var distanceToGround = hit.distance;
			model.transform.DOMoveY (model.transform.position.y + height - distanceToGround, 0);
		} else
			model.transform.DOMoveY (model.transform.position.y + 0.05f, 0);
	}
}