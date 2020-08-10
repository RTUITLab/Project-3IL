using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRay : MonoBehaviour {
    [SerializeField] GunScript Gun = null;
    [SerializeField] KeyCode ReloadButton = KeyCode.R;
    [SerializeField] int AddAmmo = 60;
    [SerializeField] Animator _topBoxAnimation = null;
    Transform Selection;
    [SerializeField] Transform gun;
    private void FixedUpdate () {
        if (Selection != null) {
            _topBoxAnimation.SetBool ("TopBox", false);
            Selection = null;
        }
        Ray ray = new Ray (gun.position, gun.forward);
        RaycastHit hit;
        // Debug.DrawLine (gun.position, gun.forward, Color.green, 2000);
        if (Physics.Raycast (ray, out hit)) {
            Transform selection = hit.transform;
            if (selection.CompareTag ("Selectable")) {
                _topBoxAnimation.SetBool ("TopBox", true);
                if (Input.GetKey (ReloadButton)) {
                    Gun.AmmoInPocket = AddAmmo;
                    Gun.ReloadAmmoInfo ();
                }
                Selection = selection;
            }
        }
    }
}