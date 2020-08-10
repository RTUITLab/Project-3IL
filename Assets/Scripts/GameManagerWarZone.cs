//using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerWarZone : MonoBehaviour {
    [SerializeField] GameObject ural;
    bool stop = false;
    void Awake () {
        Time.timeScale = 0;
    }
    void lateUpdate () {
        Time.timeScale = 0;
    }
    private void Update () {
        if (Input.GetKeyDown (KeyCode.Space) && stop == false) {
            Time.timeScale = 0;
            stop = true;
        } else if (Input.GetKeyDown (KeyCode.Space) && stop == true) {
            Time.timeScale = 1;
            stop = false;
        }
        if (Input.GetKeyDown (KeyCode.R) && stop == true) {
            Debug.Log ("reload game ");
            Destroy (ural);
            //SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
            SceneManager.LoadScene ("Menu");
        }
    }
}