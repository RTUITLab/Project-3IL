using System.Collections;
using TMPro;
using UnityEngine;
using Valve.VR;

public class GunScript : MonoBehaviour
{
    #region Settings
    [Header("Audio")]
    [SerializeField] AudioClip _realodSound = null;
    [SerializeField] AudioClip _shootsSound = null;
    [SerializeField] AudioClip _missSound = null;
    [SerializeField] AudioClip _EmptySound = null;
    [SerializeField] AudioClip[] _bulletsOnFloorSound = null;
    [SerializeField] GameObject gun = null;
    [Header("Effects")]
    [SerializeField] GameObject SandImpact = null;
    [SerializeField] GameObject StoneImpact = null;
    [SerializeField] GameObject MetalImpact = null;
    [SerializeField] GameObject BloodImpact = null;
    [SerializeField] GameObject _flashMuzzle = null;
    [SerializeField] Light[] _muzzleFlashLight = null;
    [SerializeField] ParticleSystem _muzzleFlash = null;
    [Header("Others")]
    [SerializeField] float _damage = 10f;
    [SerializeField] int ClipSize = 30;
    public int AmmoInPocket = 60;
    [SerializeField] TMP_Text _ammoText = null;
    int currentAmmo = 0;
    bool isReloading = false;
    [SerializeField] float _reloadTime = 1;
    [SerializeField] float _fireRate = 30;
    [SerializeField] Animator _topBoxAnimation = null;

    Transform Selection;
    float _nextTimetoFire = 0f;
    public AudioSource ThisAudioSource;
    public Animator WeaponAnimator;
    public Animation WeaponAnimation;
    public AnimationClip Shot;
    public SteamVR_Action_Boolean grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");

    #endregion

    private void Awake()
    {
        _ammoText = GameObject.Find("AmmoText").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        isReloading = false;
        WeaponAnimator.SetBool("Reloading", false);
        currentAmmo = ClipSize;
        ReloadAmmoInfo();
    }

    private void Update()
    {

        if (isReloading)
        {
            return;
        }
        if (Input.GetKey(KeyCode.R) && currentAmmo != 30)
        {
            StartCoroutine(Reload());
            return;
        }
        //test
        if (grabPinchAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Debug.Log("Shoot");
        }

        if ((Input.GetButton("Fire1") || (grabPinchAction.GetStateDown(SteamVR_Input_Sources.RightHand))) && Time.time >= _nextTimetoFire)
        {
            _nextTimetoFire = Time.time + 1f / _fireRate;
            if (currentAmmo > 0)
                Shoot();
            else
            {
                ThisAudioSource.PlayOneShot(_EmptySound);
                StartCoroutine(Reload()); // TODO For release, remove for stable release
            }
        }
        RaycastHit hit;
        if (Selection != null)
        {
            _topBoxAnimation.SetBool("TopBox", false);
            Selection = null;
        }
        if (Physics.Raycast(gameObject.transform.position, gun.transform.forward, out hit))
        {
            Transform selection = hit.transform;
            if (selection.CompareTag("Selectable"))
            {
                _topBoxAnimation.SetBool("TopBox", true);
                if (Input.GetKey(KeyCode.R))
                {
                    AmmoInPocket = 60;
                    ReloadAmmoInfo();
                }
                Selection = selection;

            }
        }

    }

    IEnumerator Reload()
    {
        isReloading = true;
        WeaponAnimator.SetBool("Reloading", true);
        ThisAudioSource.PlayOneShot(_realodSound);
        yield return new WaitForSeconds(_reloadTime - 0.25f);
        WeaponAnimator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        if (AmmoInPocket == 0)
        { // TODO For release, remove for stable release
            AmmoInPocket = 90;
        }
        AmmoInPocket -= (ClipSize - currentAmmo);
        currentAmmo = ClipSize;
        if (AmmoInPocket < 0)
        {
            currentAmmo += AmmoInPocket;
            AmmoInPocket = 0;
        }
        ReloadAmmoInfo();
        isReloading = false;
    }

    private void Shoot()
    {
        float changeLightGreen = Random.Range(100, 200);
        changeLightGreen /= 255;
        for (int i = 0; i < _muzzleFlashLight.Length; i++)
        {
            _muzzleFlashLight[i].color = new Color(
                _muzzleFlashLight[i].color.r, changeLightGreen, _muzzleFlashLight[i].color.b);
        }
        currentAmmo--;
        ReloadAmmoInfo();
        _flashMuzzle.SetActive(true);
        ThisAudioSource.PlayOneShot(_shootsSound);
        _muzzleFlash.Play();
        RaycastHit hit;
        StartCoroutine(ShotAnimation());
        if (Physics.Raycast(gameObject.transform.position, gun.transform.forward, out hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
            TransferDamage TD = hit.transform.GetComponent<TransferDamage>();
            if (TD != null)
            {
                TD.Damage(_damage);
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
            else if (hit.collider.tag == "Blood")
            {
                GameObject Impact = Instantiate(BloodImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Impact.transform.parent = hit.transform;
                hit.transform.GetComponent<TransferDamage>().Damage(_damage);
            }
            if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<PlayerHealth>().Damage();
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
        StartCoroutine(OffLight());
    }

    IEnumerator ShotAnimation()
    {
        yield return new WaitForSeconds(Shot.length);
        //   WeaponAnimation.Play(Shot.name);
    }

    IEnumerator OffLight()
    {
        yield return new WaitForSeconds(0.05f);
        _flashMuzzle.SetActive(false);
    }

    public void ReloadAmmoInfo()
    {
        _ammoText.text = currentAmmo.ToString() + "/" + AmmoInPocket.ToString();
    }
}