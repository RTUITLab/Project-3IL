using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class StressDeleter : MonoBehaviour
{
    [Range(0.1f, 20)]
    [SerializeField] float _howOftenResetPlayer = 1;

    [Range(0.1f, 20)]
    [SerializeField] float _howOftenResetEnemies = 1;
    List<Rigidbody> _enemiesRb = new List<Rigidbody>();
    Rigidbody _playerRb;
    private void Start()
    {
        _playerRb = GameObject.Find("Body").GetComponent<Rigidbody>();
        // get name of _rb parent
        print($"10. StressDeleter -> _rb : {_playerRb.gameObject.transform.parent.gameObject.name}");
        StartCoroutine(StopForcePlayer());
        StartCoroutine(StopForceEnemies());
    }
    void FindEnemies()
    {
        _enemiesRb.Clear();
        var enemiesScripts = FindObjectsOfType(typeof(EnemyWayChanger)) as EnemyWayChanger[];
        print($"24. StressDeleter -> enemiesScripts.Length : {enemiesScripts.Length}");
        // add enemies rigibody to list
        for (var i = 0; i < enemiesScripts.Length; i++)
        {
            _enemiesRb.Add(enemiesScripts[i].gameObject.transform.Find("Body").GetComponent<Rigidbody>());
        }
    }
    private IEnumerator StopForceEnemies()
    {
        yield return new WaitForSeconds(_howOftenResetEnemies);
        int _rbSize = _enemiesRb.Count;
        // we reset force for all enemies
        // print($"43. StressDeleter -> _rbSize : {_rbSize}");
        if (_rbSize < 2)
            FindEnemies();
        for (var i = 0; i < _rbSize; i++)
        {
            // check, if enemies was destroyed
            try
            {
                _enemiesRb[i].Sleep();
                // print($"I sleep: {_enemiesRb[i].gameObject.transform.parent.parent.gameObject.name}");
            }
            catch (MissingReferenceException)
            {
                _enemiesRb.Remove(_enemiesRb[i]);
            }
        }
        StartCoroutine(StopForceEnemies());
    }
    private IEnumerator StopForcePlayer()
    {
        yield return new WaitForSeconds(_howOftenResetPlayer);
        _playerRb.Sleep();
        StartCoroutine(StopForcePlayer());
    }
}
