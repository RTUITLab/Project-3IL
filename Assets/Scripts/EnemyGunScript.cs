using System.Collections;
using UnityEngine;
public class EnemyGunScript : MonoBehaviour
{
    #region Settings
    [Header("Audio")]
    [SerializeField] AudioClip _shootsSound = null;
    [Header("Effects")]
    [SerializeField] GameObject SandImpact = null;
    [SerializeField] GameObject StoneImpact = null;
    [SerializeField] GameObject MetalImpact = null;
    [SerializeField] GameObject BloodImpact = null;
    [SerializeField] GameObject _flashMuzzle = null;
    [SerializeField] Light[] _muzzleFlashLight = null;
    [SerializeField] ParticleSystem _muzzleFlash = null;
    [Header("Other")]
    public Transform Player;
    AudioSource _ThisAudioSource = null;
    [SerializeField] Animator animator;
    [SerializeField] float Spread = 0f;
    [SerializeField] float _fireRate = 30;
    float _nextTimetoFire = 0f;
    int Ammo = 30;
    #endregion

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _ThisAudioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < 40)
        {
            if (Time.time >= _nextTimetoFire && Ammo > 0)
            {
                float buff = _fireRate / 2;
                _nextTimetoFire = Time.time + 1f / Random.Range(_fireRate - buff, _fireRate + buff);
                Shoot();
                Ammo--;
                StartCoroutine(Shot());
            }
            else if (Ammo <= 0)
            {
                StartCoroutine(ReloadWeapon());
            }
        }
    }

    void FixedUpdate()
    {
        transform.LookAt(Player);
    }

    void Shoot()
    {
        float changeLightGreen = Random.Range(100, 200); //make random color of orange light
        changeLightGreen /= 255;
        for (int i = 0; i < _muzzleFlashLight.Length; i++)
        {
            _muzzleFlashLight[i].color = new Color(_muzzleFlashLight[i].color.r, changeLightGreen, _muzzleFlashLight[i].color.b);
        }
        _flashMuzzle.SetActive(true);
        _ThisAudioSource.PlayOneShot(_shootsSound);
        _muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(_flashMuzzle.transform.position, _flashMuzzle.transform.forward, out hit))
        {
            Debug.Log (hit.transform.name);
            if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<PlayerHealth>().Damage();
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
            else if (hit.collider.tag == "Blood")
            {
                GameObject Impact = Instantiate(BloodImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Impact.transform.parent = hit.transform;
            }
            else if (hit.collider.tag == "Metal")
            {
                GameObject Impact = Instantiate(MetalImpact, hit.point, Quaternion.LookRotation(hit.normal));
                Impact.transform.parent = hit.transform;
            }
        }
        StartCoroutine(OffLight());
    }

    IEnumerator Shot()
    {
        animator.SetBool("Shooting", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Shooting", false);
    }

    IEnumerator ReloadWeapon()
    {
        animator.SetBool("Shooting", false);
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Reloading", false);
        Ammo = 30;
    }

    IEnumerator OffLight()
    {
        yield return new WaitForSeconds(0.05f);
        _flashMuzzle.SetActive(false);
    }
}