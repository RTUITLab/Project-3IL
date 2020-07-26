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
	[Header ("Effects")]
	[SerializeField] GameObject SandImpact = null;
    [SerializeField] GameObject StoneImpact = null;
    [SerializeField] GameObject MetalImpact = null;
    [SerializeField] GameObject _flashMuzzle = null;
	[SerializeField] Light[] _muzzleFlashLight = null;
	[SerializeField] ParticleSystem _muzzleFlash = null;
	[Header ("Others")]
	[SerializeField] float _damage = 10f;
	[SerializeField] int ClipSize = 30;
	public int AmmoInPocket = 60;
	[SerializeField] TMP_Text _ammoText = null;
	int currentAmmo = 0;
	bool isReloading = false;
	[SerializeField] float _reloadTime = 1;
	[SerializeField] float _fireRate = 30;
	[SerializeField] float _impactForce = 30f;
	float _nextTimetoFire = 0f;
	AudioSource _ThisAudioSource;
    Animator _WeaponAnimator;
    LODGroup lod;
    #endregion


    void OnEnable ()
    {
		isReloading = false;
		_WeaponAnimator.SetBool ("Reloading", false);
	}

	private void Start ()
    {
		_ThisAudioSource = gameObject.GetComponent<AudioSource> ();
        _WeaponAnimator = gameObject.GetComponent<Animator>();
        currentAmmo = ClipSize;
		ReloadAmmoInfo ();
	}

	private void Update ()
    {
        if (isReloading)
        {
            return;
        }
		if (Input.GetKey(KeyCode.R) && currentAmmo != 30)
        {
			StartCoroutine (Reload ());
			return;
		}
		if (Input.GetButton("Fire1") && Time.time >= _nextTimetoFire)
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
		AmmoInPocket -= (ClipSize - currentAmmo);
		currentAmmo = ClipSize;
		if (AmmoInPocket < 0)
        {
			currentAmmo += AmmoInPocket;
			AmmoInPocket = 0;
		}
		ReloadAmmoInfo ();
		isReloading = false;
	}

	private void Shoot ()
    {
		//make random color of orange light
		float changeLightGreen = Random.Range (100, 200);
		changeLightGreen /= 255;
        for (int i = 0; i < _muzzleFlashLight.Length; i++)
        {
            _muzzleFlashLight[i].color = new Color(
                _muzzleFlashLight[i].color.r, changeLightGreen, _muzzleFlashLight[i].color.b);
        }
		//you will understand (for reload)
		currentAmmo--;
		ReloadAmmoInfo ();
		//turn on light
		_flashMuzzle.SetActive (true);
		//play shoot sound
		_ThisAudioSource.PlayOneShot (_shootsSound);
		//play partical
		_muzzleFlash.Play ();
		RaycastHit hit;

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit))
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