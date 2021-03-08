using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    public int selectedMap = 0;
    public int selectedTransport = 2;
    public int selectedBot1 = 0;
    public int selectedBot2 = 0;
    Transform spawnPlayer;
    GameObject current_transport;
    [SerializeField] GameObject[] transport;
    [SerializeField] GameObject[] Bot1;
    [SerializeField] GameObject[] Bot2;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    public void Select_map(int id)
    {
        selectedMap = id;
    }

    public void Select_bot(int id)
    {
        selectedBot1 = id;
        selectedBot2 = id;
    }

    public void Select_car(int id)
    {
        selectedTransport = id;
        SceneManager.LoadScene(selectedMap);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level != 0)
        {
            SpawnEnemies SA = GameObject.FindGameObjectWithTag("Enemy_Spawn").GetComponent<SpawnEnemies>();
            SA.Enemy1 = Bot1[selectedBot1];
            SA.Enemy2 = Bot2[selectedBot2];
            spawnPlayer = GameObject.FindGameObjectWithTag("Player_Spawn").transform;
            current_transport = Instantiate(transport[selectedTransport], spawnPlayer);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("Player_Spawn"));
            Destroy(this.gameObject);
        }
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("reload game ");
            SceneManager.LoadScene(selectedMap);
        }
    }
}