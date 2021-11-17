using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] Expressions = new Sprite[13];
    [SerializeField] private Image ExpressionLeft;
    [SerializeField] private Image ExpressionRight;

    enum Expression
    {
        Neutral,
        Sad,
        Happy,
        Angry,
        Drunk,
        Win,
        Loss,
        Shy,
        OnPhone,
        Evil,
        Scared,
        HairlessNeutral,
        HairlessAngry
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeExpressionLeft(string _mood)
    {
     //   ExpressionLeft.sprite = Expressions[(Expression)_mood];
    }

    public void ChangeExpressionRight(string _mood)
    {

    }
}
