using Assets.Plugins.UnityChairPlugin.ChairControl.ChairWork.Options;
using ChairControl.ChairWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFutuRiftController : MonoBehaviour
{
    private FutuRiftController controller;
    public UdpOptions udpOptions;
    void Start()
    {
        controller = new FutuRiftController(udpOptions, new FutuRiftOptions { interval = 50 });
        controller.Start();
    }

    void Update()
    {
        var euler = transform.eulerAngles;
        controller.Pitch = (euler.x > 150 ? euler.x - 360 : euler.x);
        controller.Roll = -(euler.z > 150 ? euler.z - 360 : euler.z);
    }

    private void OnDestroy()
    {
        controller.Stop();
    }
}
