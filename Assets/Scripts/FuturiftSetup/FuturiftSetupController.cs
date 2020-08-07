using System.Collections;
using System.Collections.Generic;
using Assets.Plugins.UnityChairPlugin.ChairControl.ChairWork.Options;
using ChairControl.ChairWork;
using UnityEngine;

public class FuturiftSetupController : MonoBehaviour
{
    public UdpOptions udpOptions;
    private FutuRiftController futuriftController;
    public PositionSetter positionSetter;
    void Start()
    {
        futuriftController = new FutuRiftController(udpOptions, new FutuRiftOptions { interval = 50 });
        futuriftController.Start();
    }

    void Update()
    {
        futuriftController.Pitch = positionSetter?.Pitch ?? 0f;
        futuriftController.Roll = positionSetter?.Roll ?? 0f;
    }

    private void OnDestroy()
    {
        futuriftController?.Stop();
    }
}
