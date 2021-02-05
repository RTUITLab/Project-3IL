using System.Collections;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [HideInInspector] public static SpawnEnemies instance;
    public GameObject Enemy1;
    public GameObject Enemy2;

    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        StartCoroutine(Spawn());
    }

    public void Spawn_En()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        Instantiate(Enemy1, gameObject.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(1.5f);
        Instantiate(Enemy2, gameObject.transform.position, gameObject.transform.rotation);
    }
}
