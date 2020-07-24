using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [HideInInspector] public static SpawnEnemies instance;
    public GameObject MotorBike;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(MotorBike, gameObject.transform.position, gameObject.transform.rotation);
    }
}
