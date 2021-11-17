using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerDialogue : MonoBehaviour
{

    [SerializeField] private Image DialogueWindow;
    [SerializeField] private GameObject TextElements;
    private float cropSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnAttackButton()
    {
        //ScaleDown();
    }

    void OnBlockButtonDown()
    {
        //ScaleUp();
    }

    public void ScaleDown()
    {
        StartCoroutine(ReduceImage());
    }

    public void ScaleUp()
    {
            StartCoroutine(EnlargeImage());
    }

    IEnumerator ReduceImage()
    {
        TextElements.SetActive(false);
        while (DialogueWindow.fillAmount > 0.65f)
        {
            DialogueWindow.fillAmount -= cropSpeed * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator EnlargeImage()
    {
        while (DialogueWindow.fillAmount < 1)
        {
            DialogueWindow.fillAmount += cropSpeed * Time.deltaTime;
            yield return null;
        }
        TextElements.SetActive(true);
    }
}
