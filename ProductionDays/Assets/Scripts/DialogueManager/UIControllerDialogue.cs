using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerDialogue : MonoBehaviour
{

    [SerializeField] private Image DialogueWindow;
    [SerializeField] private Image BackgroundWindow;
    [SerializeField] private Image ExpressionLeft;
    [SerializeField] private Image ExpressionRight;
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
        TextElements.SetActive(false);
        StartCoroutine(ReduceDialogueImage());
        StartCoroutine(ReduceBackgroundImage());
        StartCoroutine(ReduceExpressionLeft());
        StartCoroutine(ReduceExpressionRight());
    }

    public void ScaleUp()
    {
        StartCoroutine(EnlargeDialogueImage());
        StartCoroutine(EnlargeBackgroundImage());
        StartCoroutine(EnlargeExpressionLeft());
        StartCoroutine(EnlargeExpressionRight());
    }

    IEnumerator ReduceDialogueImage()
    {
        while (DialogueWindow.fillAmount > 0.5f)
        {
            DialogueWindow.fillAmount -= cropSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ReduceBackgroundImage()
    {
        while (BackgroundWindow.fillAmount > 0.5f)
        {
            BackgroundWindow.fillAmount -= cropSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ReduceExpressionLeft()
    {
        while (ExpressionLeft.fillAmount > 0.5f)
        {
            ExpressionLeft.fillAmount -= cropSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ReduceExpressionRight()
    {
        while (ExpressionRight.fillAmount > 0.5f)
        {
            ExpressionRight.fillAmount -= cropSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator EnlargeDialogueImage()
    {
        while (DialogueWindow.fillAmount < 1)
        {
            DialogueWindow.fillAmount += cropSpeed * Time.deltaTime;
            yield return null;
        }
        TextElements.SetActive(true);
    }

    IEnumerator EnlargeBackgroundImage()
    {
        while (BackgroundWindow.fillAmount < 1)
        {
            BackgroundWindow.fillAmount += cropSpeed * Time.deltaTime;
            yield return null;
        }
        TextElements.SetActive(true);
    }

    IEnumerator EnlargeExpressionLeft()
    {
        while (ExpressionLeft.fillAmount < 1)
        {
            ExpressionLeft.fillAmount += cropSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator EnlargeExpressionRight()
    {
        while (ExpressionRight.fillAmount < 1)
        {
            ExpressionRight.fillAmount += cropSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
