using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenuOnClick : MonoBehaviour
{
    public string sceneToLoad;
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
