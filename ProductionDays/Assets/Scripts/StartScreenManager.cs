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

    public void CloseGame()
    {
        Application.Quit();
    }
}
