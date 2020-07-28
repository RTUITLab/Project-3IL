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
	public AudioSource ThisAudioSource;
    public Animator WeaponAnimator;
    #endregion

	private void Start ()
    {
        isReloading = false;
        WeaponAnimator.SetBool("Reloading", false);
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
                ThisAudioSource.PlayOneShot(_EmptySound);
            }
		}
	}

	IEnumerator Reload ()
    {
		isReloading = true;
		WeaponAnimator.SetBool ("Reloading", true);
		ThisAudioSource.PlayOneShot (_realodSound);
		yield return new WaitForSeconds (_reloadTime - 0.25f);
		WeaponAnimator.SetBool ("Reloading", false);
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
		float changeLightGreen = Random.Range (100, 200);
		changeLightGreen /= 255;
        for (int i = 0; i < _muzzleFlashLight.Length; i++)
        {
            _muzzleFlashLight[i].color = new Color(
                _muzzleFlashLight[i].color.r, changeLightGreen, _muzzleFlashLight[i].color.b);
        }
		currentAmmo--;
		ReloadAmmoInfo ();
		_flashMuzzle.SetActive (true);
		ThisAudioSource.PlayOneShot (_shootsSound);
		_muzzleFlash.Play ();
		RaycastHit hit;

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit))
        {
			Target target = hit.transform.GetComponent<Target> ();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
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
        if (Random.Range(0, 100) < 5)
        {
            ThisAudioSource.PlayOneShot(_bulletsOnFloorSound[Random.Range(0, _bulletsOnFloorSound.Length)]);
        }
        else if (Random.Range(0, 100) < 2)
        {
            ThisAudioSource.PlayOneShot(_missSound);
        }
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