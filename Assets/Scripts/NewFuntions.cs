using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewFuntions : MonoBehaviour {
    bool stop = false;
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