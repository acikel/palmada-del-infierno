using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
   void OnMouseClickDown()
   {
        SceneManager.LoadScene(0);
   }
}
