using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelection : MonoBehaviour
{
    [SerializeField] GunScript _gun = null;
    [SerializeField] private Material highlightMaterial = null;
    Material defaultMaterial = null;
    [SerializeField] KeyCode _reloadButton = KeyCode.R;
    [SerializeField] int _addAmmo = 60;
    Transform _selection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
        }

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag("Selectable"))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    defaultMaterial = selectionRenderer.material;
                    selectionRenderer.material = highlightMaterial;
                    //add ammo
                    if (Input.GetKey(_reloadButton))
                    {
                        _gun.AmmoInPocket = _addAmmo;
                        _gun.ReloadAmmoInfo();
                    }
                }
                _selection = selection;
            }
        }
    }
}
