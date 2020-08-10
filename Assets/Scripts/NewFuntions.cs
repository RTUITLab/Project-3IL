using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewFuntions : MonoBehaviour {
    [SerializeField]
    bool stop = true;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start () {
        if (stop)
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
        if (stop && Input.GetKeyDown (KeyCode.R) && stop == true)
            SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

    }
}