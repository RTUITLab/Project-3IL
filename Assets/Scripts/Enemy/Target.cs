﻿using System.Collections;
using PathCreation;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject parentGO;
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
    public EnemyGunScript GunScript;
    [SerializeField] float distance = 7;
    [SerializeField] bool _NearPlayer = false;
    [SerializeField] VoiceEnemy _voiceEnemy = null;

    private void Start()
    {
        StartCoroutine(Go());
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (!_NearPlayer)
        {
            if (Vector3.Distance(Player.transform.position, gameObject.transform.position) > 200)
            {
                _pathFollower.speed = 50;
            }
            else if (Vector3.Distance(Player.transform.position, gameObject.transform.position) > 100)
            {
                _pathFollower.speed = 10;
            }
            else if (Vector3.Distance(Player.transform.position, gameObject.transform.position) > distance)
            {
                _pathFollower.speed = _speedBeforAttack;
            }
            else
            {
                StartCoroutine(Speed());
                _pathFollower.speed = _normalSpeed;
                _NearPlayer = true;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        //with 15% chance, terrorist say something
        if (Random.Range(0, 100) < 15)
        {
            if (_voiceEnemy != null)
            {
                _voiceEnemy.PlayVoice();
            }
        }
        if (_health > 0)
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
        }
    }

    void Die()
    {
        if (GunScript != null)
        {
            GunScript.enabled = false;
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 1)
        {
            SpawnEnemies.instance.Spawn_En();
        }

        _Explosion.gameObject.SetActive(true);
        _Fire.Stop();
        thisAudioSource.Play();
        Destroy(Body);
        Destroy(parentGO, 3f);
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(_delayBeforeStart);
        _pathFollower.enabled = true;
    }

    IEnumerator Speed()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            _pathFollower.speed = 1.45f;
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            _pathFollower.speed = 1.55f;
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            _pathFollower.speed = 1.47f;
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            _pathFollower.speed = 1.52f;
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            _pathFollower.speed = 1.4f;
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            _pathFollower.speed = 1.52f;
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            _pathFollower.speed = 1.5f;
        }
    }
}
