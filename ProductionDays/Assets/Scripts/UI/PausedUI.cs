using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedUI : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }


    public void Resume()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);        
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
