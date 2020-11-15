using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressDeleter : MonoBehaviour {
    [SerializeField] float timer = 2f;
    Rigidbody rb;
    bool stable = true;
    private void Start () {
        rb = this.gameObject.GetComponent<Rigidbody> ();
        rb.Sleep ();
        StartCoroutine (unFreezeRotation (timer));
    }
    IEnumerator unFreezeRotation (float time) {
        yield return new WaitForSeconds (time);
        rb.velocity = new Vector3 (0, 0, 0);
        rb.freezeRotation = false;
        // StartCoroutine (offStable ());
    }
    IEnumerator offStable () {
        yield return new WaitForSeconds (10f);
    }
    private void FixedUpdate () {
        if (stable)
            rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, rb.velocity.z) / 2;
    }
}