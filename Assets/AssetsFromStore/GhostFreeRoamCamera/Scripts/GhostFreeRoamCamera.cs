using UnityEngine;

[RequireComponent (typeof (Camera))]
public class GhostFreeRoamCamera : MonoBehaviour {
	[SerializeField] float recoilSpeed = 30f;
	[SerializeField] int _addAmmo = 60;
	[SerializeField] private string selectableTag = "Selectable";
	[SerializeField] GunScript _gun = null;
	[SerializeField] KeyCode _reloadButton = KeyCode.R;
	[SerializeField] AudioClip _hummockSound = null;
	[SerializeField] AudioSource _thisAudioSource = null;
	[SerializeField] StressReceiver stressReceiver = null;
	[SerializeField] float _earthquakeRate = 1;
	float _nextTimetoFire = 0f;
	[Tooltip ("Seconds to wait before trigerring the explosion particles and the trauma effect")]
	public float Delay = 1;
	[Tooltip ("Maximum stress the effect can inflict upon objects Range([0,1])")]
	public float MaximumStress = 0.6f;
	[Tooltip ("Maximum distance in which objects are affected by this TraumaInducer")]
	public float Range = 45;

	Transform _selection;
	[SerializeField] private Material highlightMaterial = null;
	Material defaultMaterial = null;
	float _upRecoil = 0;
	float _sideRecoil = 0;
	public float initialSpeed = 10f;
	public float increaseSpeed = 1.25f;

	public bool allowMovement = true;
	public bool allowRotation = true;

	public KeyCode forwardButton = KeyCode.W;
	public KeyCode backwardButton = KeyCode.S;
	public KeyCode rightButton = KeyCode.D;
	public KeyCode leftButton = KeyCode.A;

	public float cursorSensitivity = 0.025f;
	public bool cursorToggleAllowed = true;
	public KeyCode cursorToggleButton = KeyCode.Escape;

	private float currentSpeed = 0f;
	private bool moving = false;
	private bool togglePressed = false;

	[System.Obsolete]
	private void OnEnable () {
		if (cursorToggleAllowed) {
			Screen.lockCursor = true;
			Cursor.visible = false;
		}
	}

	[System.Obsolete]
	private void Update () {

		// make earthquake
		if (Input.GetKey (KeyCode.Space) && Time.time >= _nextTimetoFire) {
			_nextTimetoFire = Time.time + 1f / _earthquakeRate;
			_thisAudioSource.PlayOneShot (_hummockSound);
			var receiver = stressReceiver;
			float distance01 = Mathf.Clamp01 (0 / Range);
			float stress = (1 - Mathf.Pow (distance01, 2)) * MaximumStress;
			receiver.InduceStress (stress);

		}
		#region  script for select object
		//https://youtu.be/_yf5vzZ2sYE

		if (_selection != null) {
			var selectionRenderer = _selection.GetComponent<Renderer> ();
			selectionRenderer.material = defaultMaterial;
			_selection = null;
		}

		var ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2f, Screen.height / 2f, 0f));
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			var selection = hit.transform;
			if (selection.CompareTag (selectableTag)) {
				var selectionRenderer = selection.GetComponent<Renderer> ();
				if (selectionRenderer != null) {
					defaultMaterial = selectionRenderer.material;
					selectionRenderer.material = highlightMaterial;
					//add ammo
					if (Input.GetKey (_reloadButton)) {
						_gun._AmmoInPocket = _addAmmo;
						_gun.ReloadAmmoInfo ();
					}
				}
				_selection = selection;
			}
		}
		#endregion

		if (allowMovement) {
			bool lastMoving = moving;
			Vector3 deltaPosition = Vector3.zero;

			if (moving)
				currentSpeed += increaseSpeed * Time.deltaTime;

			moving = false;

			CheckMove (forwardButton, ref deltaPosition, transform.forward);
			CheckMove (backwardButton, ref deltaPosition, -transform.forward);
			CheckMove (rightButton, ref deltaPosition, transform.right);
			CheckMove (leftButton, ref deltaPosition, -transform.right);

			if (moving) {
				if (moving != lastMoving)
					currentSpeed = initialSpeed;

				transform.position += deltaPosition * currentSpeed * Time.deltaTime;
			} else currentSpeed = 0f;
		}

		if (allowRotation) {
			Vector3 eulerAngles = transform.eulerAngles;
			eulerAngles.x += -Input.GetAxis ("Mouse Y") * 359f * cursorSensitivity - _upRecoil;
			eulerAngles.y += Input.GetAxis ("Mouse X") * 359f * cursorSensitivity - _sideRecoil;
			_sideRecoil -= recoilSpeed * Time.deltaTime;
			_upRecoil -= recoilSpeed * Time.deltaTime;
			if (_sideRecoil < 0)
				_sideRecoil = 0;
			if (_upRecoil < 0)
				_upRecoil = 0;
			transform.eulerAngles = eulerAngles;
		}
		_upRecoil = 0;
		_sideRecoil = 0;
		if (cursorToggleAllowed) {
			if (Input.GetKey (cursorToggleButton)) {
				if (!togglePressed) {
					togglePressed = true;
					Screen.lockCursor = !Screen.lockCursor;
					Cursor.visible = !Cursor.visible;
				}
			} else togglePressed = false;
		} else {
			togglePressed = false;
			Cursor.visible = false;
		}
	}

	private void CheckMove (KeyCode keyCode, ref Vector3 deltaPosition, Vector3 directionVector) {
		if (Input.GetKey (keyCode)) {
			moving = true;
			deltaPosition += directionVector;
		}
	}
	public void addRecoil (float upRecoil, float sideRecoil) {
		_upRecoil += upRecoil;
		_sideRecoil += sideRecoil;
	}
}