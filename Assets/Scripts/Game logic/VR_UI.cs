﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class VR_UI : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}