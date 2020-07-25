using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [HideInInspector] public static SpawnEnemies instance;
    public GameObject Enemy_prefab;

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
        Instantiate(Enemy_prefab, gameObject.transform.position, gameObject.transform.rotation);
    }
}
