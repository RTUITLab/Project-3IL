using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadWarZone : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("reload game ");
            SceneManager.LoadScene("WarZone");
        }
    }
    public void loadWarzone()
    {
        SceneManager.LoadScene("WarZone");
    }
}
