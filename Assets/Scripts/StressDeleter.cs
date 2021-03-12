using System.Collections;
using UnityEngine;

public class StressDeleter : MonoBehaviour
{
    [SerializeField] byte _time = 1;
    Rigidbody _rb;
    private void Start()
    {
        _rb = GameObject.Find("Body").GetComponent<Rigidbody>();
        // get name of _rb parent
        print($"10. StressDeleter -> _rb : {_rb.gameObject.transform.parent.gameObject.name}");
        StartCoroutine(StopForce());
    }

    private IEnumerator StopForce()
    {
        yield return new WaitForSeconds(_time);
        _rb.Sleep();
        StartCoroutine(StopForce());
    }
}
