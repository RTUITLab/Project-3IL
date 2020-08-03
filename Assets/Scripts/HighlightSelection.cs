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
    Transform Selection;
    private Transform Camera;


    private void Start()
    {
        Camera = gameObject.transform;
    }

    private void FixedUpdate()
    {
        if (Selection != null)
        {
            var selectionRenderer = Selection.GetComponent<Renderer>();
            selectionRenderer.material = DefaultMaterial;
            Selection = null;
        }
        Ray ray = new Ray(Camera.position, Camera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Transform selection = hit.transform;
            if (selection.CompareTag("Selectable"))
            {
                Renderer selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    DefaultMaterial = selectionRenderer.material;
                    selectionRenderer.material = HighlightMaterial;
                    if (Input.GetKey(ReloadButton))
                    {
                        Gun.AmmoInPocket = AddAmmo;
                        Gun.ReloadAmmoInfo();
                    }
                }
                Selection = selection;
            }
        }
    }
}
