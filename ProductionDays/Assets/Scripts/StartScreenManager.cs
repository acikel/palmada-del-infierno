using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    [SerializeField] private int _gameSceneIndex;
    public void StartGame()
    {
        SceneManager.LoadScene(_gameSceneIndex);
    }

    private void Start()
    {
        AudioManager.Instance.PlayGameMusic();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoCredits()
    {

        SceneManager.LoadScene(1);
    }
}
