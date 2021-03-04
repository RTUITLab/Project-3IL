using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] float _fireRate = 30;
    Dictionary<string, GameObject> effects = new Dictionary<string, GameObject>();
    float _nextTimetoFire = 0f;
    int Ammo = 30;
    public bool invert = false;
    #endregion

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _ThisAudioSource = gameObject.GetComponent<AudioSource>();
        effects.Add("Sand", SandImpact);
        effects.Add("Stone", StoneImpact);
        effects.Add("Untagged", StoneImpact);
        effects.Add("Metal", MetalImpact);
        effects.Add("Blood", BloodImpact);
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
                StartCoroutine(ShootAnim());
            }
            else if (Ammo <= 0)
            {
                StartCoroutine(ReloadWeapon());
            }
        }
    }

    void FixedUpdate()
    {
        //this.transform.LookAt(Player);
    }

    void Shoot()
    {
        Debug.DrawLine(transform.position, Player.position, Color.blue, 1);
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
        if (!invert)
        {
            if (Physics.Raycast(transform.position, Vector3.forward, out hit))
                Hit(hit);
        }
        else
        {
            if (Physics.Raycast(transform.position, -Vector3.forward, out hit))
                Hit(hit);
        }
        StartCoroutine(OffLight());
    }

    IEnumerator ShootAnim()
    {
        if (animator)
        {
            animator.SetBool("Shooting", true);
            yield return new WaitForSeconds(1);
            animator.SetBool("Shooting", false);
        }
    }

    IEnumerator ReloadWeapon()
    {
        if (animator)
        {
            animator.SetBool("Shooting", false);
            animator.SetBool("Reloading", true);
            yield return new WaitForSeconds(1.5f);
            animator.SetBool("Reloading", false);
        }
        Ammo = 30;
    }

    IEnumerator OffLight()
    {
        yield return new WaitForSeconds(0.05f);
        _flashMuzzle.SetActive(false);
    }
    void Hit(RaycastHit hit)
    {
        Debug.Log(hit.transform.name);
        if (hit.transform.tag == "Player")
        {
            hit.transform.GetComponent<PlayerHealth>().Damage();
        }
        else if (effects.ContainsKey(hit.transform.tag))
        {
            GameObject Impact = Instantiate(effects[hit.collider.tag], hit.point, Quaternion.LookRotation(hit.normal));
            Impact.transform.parent = hit.transform;
        }
    }
}
