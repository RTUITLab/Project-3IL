using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Valve.VR;

public class HandSelector : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObject;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            var content = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "handIndex.txt"));
            var handNum = int.Parse(content);
            trackedObject.index = (SteamVR_TrackedObject.EIndex)Enum.Parse(typeof(SteamVR_TrackedObject.EIndex), $"Device{handNum}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error while apply hand {ex.Message}");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
