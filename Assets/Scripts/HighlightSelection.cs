using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelection : MonoBehaviour
{
    [SerializeField] GunScript Gun = null;
    [SerializeField] private Material HighlightMaterial = null;
    Material DefaultMaterial = null;
    [SerializeField] KeyCode ReloadButton = KeyCode.R;
    [SerializeField] int AddAmmo = 60;
    [SerializeField] Animator _topBoxAnimation=null;
    Transform Selection;

    //old method
    //wait, for what it?
    //private Transform Camera;
    //private void Start() => Camera = gameObject.transform;

    private void FixedUpdate()
    {
        if (Selection != null )
        {
        _topBoxAnimation.SetBool("TopBox",false);
        //     var selectionRenderer = Selection.GetComponent<Renderer>();
        //     selectionRenderer.material = DefaultMaterial;
        //     Selection = null;
        }

        //old method
        //Ray ray = new Ray(Camera.position, Camera.forward);
        //new method
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Transform selection = hit.transform;
            if (selection.CompareTag("Selectable"))
            {
               //Renderer selectionRenderer = selection.GetComponent<Renderer>();
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
