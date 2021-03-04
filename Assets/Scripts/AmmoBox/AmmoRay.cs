using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRay : MonoBehaviour
{
    [SerializeField] GunScript Gun = null;
    Material DefaultMaterial = null;
    [SerializeField] KeyCode ReloadButton = KeyCode.R;
    [SerializeField] int AddAmmo = 60;
    [SerializeField] Animator _topBoxAnimation = null;
    Transform Selection;

    //old method
    //wait, for what it?
    private Transform Camera;
    private void Start()
    {
        _topBoxAnimation = GameObject.Find("Box_Top").GetComponent<Animator>();
        Camera = gameObject.transform;
    }

    private void FixedUpdate()
    {
        if (Selection != null)
        {
            _topBoxAnimation.SetBool("TopBox", false);
            Selection = null;
        }

        Ray ray = new Ray(Camera.position, Camera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform selection = hit.transform;
            if (selection.CompareTag("Player"))
            {
                _topBoxAnimation.SetBool("TopBox", true);
                if (Gun.AmmoInPocket == 0 && _topBoxAnimation.GetBool("TopBox"))
                {
                    Gun.AmmoInPocket = AddAmmo;
                    Gun.ReloadAmmoInfo();
                }
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
