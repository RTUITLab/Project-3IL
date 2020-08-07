using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSetup : MonoBehaviour
{
    public GameObject placer;
    // Start is called before the first frame update
    void Start()
    {

    }
    float PitchByRollh1(float Pitch) => (11 * Pitch + 198) / 9;
    float RollByPitch1(float Roll) => (9 * Roll - 198) / 11;

    void OnMouseOver()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            var position = new Vector3(hit.point.x, 3, hit.point.z);
            placer.transform.position = position;
            Debug.DrawLine(Vector3.up * 3, position);

            if (position.z < 0 && position.x > 0)
            {
                var withPitch = new Vector3(PitchByRollh1(position.z), 4, position.z);
                var withRoll = new Vector3(position.x, 4, RollByPitch1(position.x));
                Debug.DrawLine(Vector3.up * 4, withPitch, Color.red);
                Debug.DrawLine(Vector3.up * 4, withRoll, Color.green);

                placer.transform.position = (withRoll + withPitch) / 2;

            }

        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
