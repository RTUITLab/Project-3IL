using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSetter : MonoBehaviour
{
    private FuturiftSetupController controller;
    private Renderer renderer;
    public float Pitch => transform.position.x;
    public float Roll => transform.position.z;
    // Start is called before the first frame update
    void Start()
    {
        this.controller = FindObjectOfType<FuturiftSetupController>();
        this.renderer = GetComponent<Renderer>();
    }

    void OnMouseDown()
    {
        controller.positionSetter = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (controller.positionSetter == this)
        {
            transform.localScale = Vector3.one * 2;
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}
