using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeToBlackDuration = 1f;
    [SerializeField] private float fadeToGameDuration = 0.5f;

    private void Awake()
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 1f;
        InstanceRepository.Instance.Add(this);
        
        SceneManager.sceneLoaded +=SceneManagerOnsceneLoaded;
        FadeToGame();
    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FadeToGame();
    }

    public void FadeToBlack(Action callbackAction = null)
    {
        canvasGroup.DOFade(1f, fadeToBlackDuration).OnComplete((() =>
        {
            callbackAction?.Invoke();
        }));
    }

    public void FadeToGame(Action callbackAction = null)
    {
        canvasGroup.DOFade(0f, fadeToGameDuration).OnComplete((() =>
        {
            callbackAction?.Invoke();
        }));
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -=SceneManagerOnsceneLoaded;
        InstanceRepository.Instance.Remove(this);
    }
}
