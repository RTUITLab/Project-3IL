using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressDeleter : MonoBehaviour
{
    [SerializeField] float timer = 2f;
    Rigidbody rb;
    bool stable = true;
    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.Sleep();
        StartCoroutine(unFreezeRotation(timer));
    }
    IEnumerator unFreezeRotation(float time)
    {
        yield return new WaitForSeconds(time);
        rb.freezeRotation = false;
        stable = false;
    }
    private void Update()
    {
        if (stable)
        {
            rb.isKinematic = true;
            rb.isKinematic = false;
        }
    }
}