using System.Collections;
using TMPro;
using UnityEngine;
public class EnemyGunScript : MonoBehaviour
{
    #region Settings
    [Header ("Audio")]
    [SerializeField] AudioClip _shootsSound = null;
    [Header ("Effects")]
    [SerializeField] GameObject SandImpact = null;
    [SerializeField] GameObject StoneImpact = null;
    [SerializeField] GameObject MetalImpact = null;
    [SerializeField] GameObject _flashMuzzle = null;
    [SerializeField] Light[] _muzzleFlashLight = null;
    [SerializeField] ParticleSystem _muzzleFlash = null;
    [Header ("Others")]
    [SerializeField] Transform Player;
    [SerializeField] float _range = 100f;
    AudioSource _ThisAudioSource = null;
    [SerializeField] float Spread = 0f;
    [SerializeField] float _fireRate = 30;
    float _nextTimetoFire = 0f;
    #endregion

    private void Start()
    {
        _ThisAudioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Update ()
    {
        if (Time.time >= _nextTimetoFire)
        {
            var buff = _fireRate / 2;
            _nextTimetoFire = Time.time + 1f / Random.Range (_fireRate - buff, _fireRate + buff);
            Shoot ();
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    ///  enemy look at player
    void FixedUpdate()
    {
        transform.LookAt(Player);
    }

    void Shoot ()
    {
        //make random color of orange light
        float changeLightGreen = Random.Range (100, 200);
        changeLightGreen /= 255;
        for (int i = 0; i < _muzzleFlashLight.Length; i++)
        {
            _muzzleFlashLight[i].color = new Color(_muzzleFlashLight[i].color.r, changeLightGreen, _muzzleFlashLight[i].color.b);
        }
        //turn on light
        _flashMuzzle.SetActive (true);
        //play shoot sound
        _ThisAudioSource.PlayOneShot (_shootsSound);
        //play partical
        _muzzleFlash.Play ();
        RaycastHit hit;
        Vector3 spread = new Vector3 (Random.Range (0, Spread), Random.Range (0, Spread), Random.Range (0, Spread));
        Debug.DrawRay(transform.position, transform.forward + spread, Color.red);
        if (Physics.Raycast(transform.position, transform.forward + spread,  out hit, _range))
        {
            Debug.Log (hit.transform.name);
            // Target target = hit.transform.GetComponent<Target> ();
            // if (target != null)
            //     target.TakeDamage (_damage);
            //we can add force to rigibody
            // if (hit.rigidbody != null)
            //     hit.rigidbody.AddForce (hit.normal * _impactForce);
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
        StartCoroutine (OffLight ());
    }

    IEnumerator OffLight ()
    {
        yield return new WaitForSeconds (0.05f);
        _flashMuzzle.SetActive (false);
    }
}