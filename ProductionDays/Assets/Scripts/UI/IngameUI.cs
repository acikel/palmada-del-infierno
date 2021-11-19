using System;
using DG.Tweening;
using UnityEngine;


public class IngameUI : MonoBehaviour
{
    [SerializeField] private GameObject wastedUI;
    [SerializeField] private GameObject pausedUI;
    
    private void Awake()
    {
        InstanceRepository.Instance.Add(this);
    }

    private void Start()
    {
        wastedUI.SetActive(false);
    }

    public void ShowWasted()
    {
        var canvasGroup = wastedUI.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.DOFade(1f, 2f);
        }
        
        wastedUI.SetActive(true);
    }

    public void TogglePauseMenu()
    {
        bool showing = pausedUI.activeInHierarchy;
        if (showing)
        {
            Time.timeScale = 1;
            pausedUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pausedUI.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        InstanceRepository.Instance.Remove(this);
    }
}
