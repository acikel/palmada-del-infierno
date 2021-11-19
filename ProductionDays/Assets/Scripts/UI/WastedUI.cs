using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WastedUI : MonoBehaviour
{
    private Animator animator;
    private bool glitching = true;

    public Animator Animator
    {
        get
        {
            return animator ??= GetComponent<Animator>();
        }
    }

    private void Start()
    {
        StartCoroutine(RandomGlitching());
    }

    private IEnumerator RandomGlitching()
    {
        while (glitching)
        {
            yield return new WaitForSecondsRealtime(Random.Range(2, 8));
            Animator.SetTrigger("Glitch");
        }
    }

    public void RestartFromCheckpoint()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        glitching = false;
        StopAllCoroutines();
    }
}
