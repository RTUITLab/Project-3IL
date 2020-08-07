using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRay : MonoBehaviour
{
    [SerializeField] GunScript Gun = null;
    [SerializeField] KeyCode ReloadButton = KeyCode.R;
    [SerializeField] int AddAmmo = 60;
    [SerializeField] Animator _topBoxAnimation=null;
    Transform Selection;
    private Transform Camera;
    private void Start() => Camera = gameObject.transform;
    private void FixedUpdate()
    {
        if (Selection != null )
        {
        _topBoxAnimation.SetBool("TopBox",false);

        Selection = null;
        }
        Ray ray = new Ray(Camera.position, Camera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Transform selection = hit.transform;
            if (selection.CompareTag("Selectable"))
            {
                _topBoxAnimation.SetBool("TopBox",true);
                if (Input.GetKey(ReloadButton))
                {
                    Gun.AmmoInPocket = AddAmmo;
                    Gun.ReloadAmmoInfo();
                }
                Selection = selection;
            }
        }
    }
}
