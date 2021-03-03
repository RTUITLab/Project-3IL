using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("reload game ");
            SceneManager.LoadScene("WarZone");
        }
    }

    public void loadScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}
