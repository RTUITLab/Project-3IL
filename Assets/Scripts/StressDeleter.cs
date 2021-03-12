using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

public class StressDeleter : MonoBehaviour
{
    [SerializeField] byte _howOftenResetPlayer = 1;
    List<Rigidbody> _enemiesRb = new List<Rigidbody>();
    Rigidbody _rb;
    private void Start()
    {
        _rb = GameObject.Find("Body").GetComponent<Rigidbody>();
        // get name of _rb parent
        print($"10. StressDeleter -> _rb : {_rb.gameObject.transform.parent.gameObject.name}");
        StartCoroutine(StopForce());
        StartCoroutine(FindEnemies());
    }
    IEnumerator FindEnemies()
    {
        yield return new WaitForSeconds(5f);
        var enemiesScripts = FindObjectsOfType(typeof(EnemyWayChanger)) as EnemyWayChanger[];
        print($"24. StressDeleter -> enemiesScripts.Length : {enemiesScripts.Length}");
        // add enemies rigibody to list
        for (var i = 0; i < enemiesScripts.Length; i++)
        {
            _enemiesRb.Add(enemiesScripts[i].gameObject.transform.Find("Body").GetComponent<Rigidbody>());
        }
    }
    private IEnumerator StopForce()
    {
        yield return new WaitForSeconds(_howOftenResetPlayer);
        _rb.Sleep();
        int _rbSize = _enemiesRb.Count;
        // we reset force for all enemies
        for (var i = 0; i < _rbSize; ++i)
        {
            // check, if enemies was destroyed
            try
            {
                if (_rbSize < 2)
                    FindEnemies();
                _enemiesRb[i].Sleep();
            }
            catch (MissingReferenceException)
            {
                _enemiesRb.Remove(_enemiesRb[i]);
                FindEnemies();
            }
        }
        StartCoroutine(StopForce());
    }
}
