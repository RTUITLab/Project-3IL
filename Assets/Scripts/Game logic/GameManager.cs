//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] transport;
    Transform spawnPlayer;
    GameObject current_transport;
    public int selected = 2;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level != 0)
        {
            spawnPlayer = GameObject.FindGameObjectWithTag("Player_Spawn").transform;
            current_transport = Instantiate(transport[selected], spawnPlayer);
        }
        // найти спавнер ботов и заменить префабы
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("reload game ");
            Destroy(current_transport);
            SceneManager.LoadScene("Menu");
            Destroy(this.gameObject);
        }
    }
}