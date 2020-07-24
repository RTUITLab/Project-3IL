using System.Collections;
using TMPro;
using UnityEngine;
public class GunScript : MonoBehaviour
{
	#region Settings
	[Header ("Audio")]
	[SerializeField] AudioClip _realodSound = null;
	[SerializeField] AudioClip _shootsSound = null;
	[SerializeField] AudioClip _missSound = null;
	[SerializeField] AudioClip _EmptySound = null;
	[SerializeField] AudioClip[] _bulletsOnFloorSound = null;
	[Header ("Camera")]
	[SerializeField] Camera _fpsCam = null;
	[Header ("Effects")]
	[SerializeField] GhostFreeRoamCamera _FpsCamScript = null;
	[SerializeField] GameObject SandImpact = null;
    [SerializeField] GameObject StoneImpact = null;
    [SerializeField] GameObject MetalImpact = null;
    [SerializeField] GameObject _flashMuzzle = null;
	[SerializeField] Light[] _muzzleFlashLight = null;
	[SerializeField] ParticleSystem _muzzleFlash = null;
	[Header ("Others")]
	[SerializeField] float _damage = 10f;
	[SerializeField] float _range = 100f;
	[SerializeField] int _maxAmmo = 30;
	public int AmmoInPocket = 60;
	[SerializeField] TMP_Text _ammoText = null;
	int currentAmmo = 0;
	bool isReloading = false;
	[SerializeField] Animator _WeaponAnimator = null;
	[SerializeField] float _reloadTime = 1;
	[SerializeField] float _fireRate = 30;
	[SerializeField] float _impactForce = 30f;

	[Header ("Recoil")]
	[SerializeField] float _MinUpRecoil = 1;
	[SerializeField] float _MaxUpRecoil = 5;
	[SerializeField] float _MaxSideRecoil = 5;
	[SerializeField] float _MinSideRecoil = 1;
	[Header ("Spread")]
	[SerializeField] float _YSpread = 1;
	[SerializeField] float _XSpread = 1;
	[SerializeField] float _SpreadTimeUp = 0.1f;
	[SerializeField] float _SpreadTimeDown = 0.2f;
	float _WithTimeMoreSpread = 0;
	float _nextTimetoFire = 0f;
	AudioSource _ThisAudioSource;
	#endregion
	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable ()
    {
		// fix some bug with reload, if we will have more weapons
		isReloading = false;
		_WeaponAnimator.SetBool ("Reloading", false);
	}

	private void Start ()
    {
		_ThisAudioSource = gameObject.GetComponent<AudioSource> ();
		currentAmmo = _maxAmmo;
		ReloadAmmoInfo ();
	}

	private void Update ()
    {
        if (isReloading)
        {
            return;
        }
		if ((currentAmmo <= 0 &&
				AmmoInPocket > 0) || (Input.GetKey (KeyCode.R) && currentAmmo != 30))
        {
			StartCoroutine (Reload ());
			return;
		}
        if (_WithTimeMoreSpread > 0)
        {
            _WithTimeMoreSpread -= _SpreadTimeDown;
        }
		if (Input.GetButton ("Fire1") && Time.time >= _nextTimetoFire)
        {
			_nextTimetoFire = Time.time + 1f / _fireRate;
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                _ThisAudioSource.PlayOneShot(_EmptySound);
            }
		}

	}

	IEnumerator Reload ()
    {
		isReloading = true;
		_WeaponAnimator.SetBool ("Reloading", true);
		Debug.Log ("Reloading...");
		_ThisAudioSource.PlayOneShot (_realodSound);
		yield return new WaitForSeconds (_reloadTime - 0.25f);
		_WeaponAnimator.SetBool ("Reloading", false);
		yield return new WaitForSeconds (0.25f);
		AmmoInPocket -= (_maxAmmo - currentAmmo);
		currentAmmo = _maxAmmo;
		if (AmmoInPocket < 0)
        {
			currentAmmo += AmmoInPocket;
			AmmoInPocket = 0;
		}
		ReloadAmmoInfo ();
		isReloading = false;
	}

	void Shoot ()
    {
		//make random color of orange light
		float changeLightGreen = Random.Range (100, 200);
		changeLightGreen /= 255;
		for (int i = 0; i < _muzzleFlashLight.Length; i++)
			_muzzleFlashLight[i].color = new Color (
				_muzzleFlashLight[i].color.r, changeLightGreen, _muzzleFlashLight[i].color.b);
		//you will understand (for reload)
		currentAmmo--;
		ReloadAmmoInfo ();
		//turn on light
		_flashMuzzle.SetActive (true);
		//play shoot sound
		_ThisAudioSource.PlayOneShot (_shootsSound);
		//make recoil
		_FpsCamScript.addRecoil (
			Random.Range (_MinUpRecoil, _MaxUpRecoil) / 5f, Random.Range (_MinSideRecoil, _MaxSideRecoil) / 5f);
		//play partical
		_muzzleFlash.Play ();
		RaycastHit hit;

        if (Physics.Raycast(_fpsCam.transform.position + new Vector3(
                    Random.Range(-_XSpread, _XSpread) + _WithTimeMoreSpread,
                    Random.Range(-_YSpread, _YSpread) + _WithTimeMoreSpread),
                _fpsCam.transform.forward, out hit, _range))
        {
			//  Debug.Log (hit.transform.name);
			Target target = hit.transform.GetComponent<Target> ();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
            //we can add force to rigibody
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.normal * _impactForce);
            }
            if (hit.collider.tag == "Sand")
            {
                GameObject Impact = Instantiate(SandImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Impact.transform.parent = hit.transform;
            }
            else if (hit.collider.tag == "Stone" || hit.collider.tag == "Untagged")
            {
                GameObject Impact = Instantiate(StoneImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Impact.transform.parent = hit.transform;
            }
            else if (hit.collider.tag == "Metal")
            {
                GameObject Impact = Instantiate(MetalImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Impact.transform.parent = hit.transform;
            }
        }
        //add spread
        // _WithTimeMoreSpread += _SpreadTimeUp;
        // we play bullets on floor sound with 5% chance
        if (Random.Range(0, 100) < 5)
        {
            _ThisAudioSource.PlayOneShot(_bulletsOnFloorSound[Random.Range(0, _bulletsOnFloorSound.Length)]);
        }
        // we play miss sound with 2% chance
        if (Random.Range(0, 100) < 2)
        {
            _ThisAudioSource.PlayOneShot(_missSound);
        }
		//turn off light
		StartCoroutine (OffLight ());
	}

	IEnumerator OffLight ()
    {
		yield return new WaitForSeconds (0.05f);
		_flashMuzzle.SetActive (false);
	}

    public void ReloadAmmoInfo()
    {
        _ammoText.text = currentAmmo.ToString() + "/" + AmmoInPocket.ToString();
    }
}