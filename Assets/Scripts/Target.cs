using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
	[SerializeField] float _health = 100f;
	[SerializeField] ParticleSystem _Explosion = null;
	[SerializeField] ParticleSystem _Fire = null;
	[SerializeField] AudioSource thisAudioSource = null;
	[SerializeField] GameObject Body = null;
	[SerializeField] PathFollower _pathFollower = null;
	[SerializeField] float _delayBeforeStart = 0;
	[SerializeField] float _speedBeforAttack = 5;
	[SerializeField] float _normalSpeed = 1.5f;
	public GameObject Player = null;
	[SerializeField] float distance = 7;
	[SerializeField] bool _NearPlayer = false;

    private void Start()
    {
        StartCoroutine(Go());
        Player = GameObject.FindGameObjectWithTag("Player");
    }

	IEnumerator Go ()
    {
		yield return new WaitForSeconds (_delayBeforeStart);
		_pathFollower.enabled = true;
	}

	//when we get damage
	public void TakeDamage (float amount)
    {
		_health -= amount;
        if (_health < 100 && !_Fire.gameObject.activeInHierarchy)
        {
            _Fire.gameObject.SetActive(true);
        }
        else if (_health <= 0f)
        {
            Die();
        }
		Debug.Log ($"Enemy Health - {_health}");
	}

	private void FixedUpdate ()
    {
        if (_NearPlayer)
        {
            return;
        }
        //check distance before player, if it big, we up speed
        if (Player.transform.position.x + Player.transform.position.z - transform.position.x - transform.position.z > distance)
        {
            _pathFollower.speed = _speedBeforAttack;
        }
        else
        {
            _pathFollower.speed = _normalSpeed;
            _NearPlayer = true;
        }
	}

	void Die ()
    {
        SpawnEnemies.instance.Spawn();
		_Explosion.gameObject.SetActive (true);
		_Fire.Stop ();
		thisAudioSource.Play ();
		Destroy (Body, 2f);
		Destroy (gameObject, 2f);
	}
}