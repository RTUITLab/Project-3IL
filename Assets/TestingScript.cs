using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public Transform flWheel;
    public Transform frWheel;
    public Transform blWheel;
    public Transform brWheel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(flWheel.position, brWheel.position, Color.red, 0.1f, false);
    }
}
