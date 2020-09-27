
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [HideInInspector] public static SpawnEnemies instance;
    public GameObject Enemy_prefab;
    public GameObject BTR_prefab;

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
        Instantiate(BTR_prefab, gameObject.transform.position, gameObject.transform.rotation);
    }
}
