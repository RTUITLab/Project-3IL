using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBeforeStartChair : MonoBehaviour {
    [SerializeField] bool StartIt = false;
    [SerializeField] float time = 2f;
    CarFutuRiftController carFutuRiftController;
    void Start () {
        carFutuRiftController = this.gameObject.GetComponent<CarFutuRiftController> ();
        if (StartIt)
            StartCoroutine (CanRotation ());
    }
    IEnumerator CanRotation () {
        yield return new WaitForSeconds (time);
        carFutuRiftController.enabled = true;
    }
}