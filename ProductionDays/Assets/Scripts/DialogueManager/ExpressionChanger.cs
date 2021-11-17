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
        ExpressionLeft.sprite = Expressions[GetExpression(_mood)];
    }

    public void ChangeExpressionRight(string _mood)
    {
        ExpressionLeft.sprite = Expressions[GetExpression(_mood)];
    }

    private int GetExpression(string _mood)
    {
        int _expression = 0;

        switch (_mood)
        {
            case "Neutral":
                _expression = 0;
                break;

            case "Sad":
                _expression = 1;
                break;

            case "Happy":
                _expression = 2;
                break;

            case "Angry":
                _expression = 3;
                break;

            case "Drunk":
                _expression = 4;
                break;

            case "Win":
                _expression = 5;
                break;

            case "Loss":
                _expression = 6;
                break;

            case "Shy":
                _expression = 7;
                break;

            case "OnPhone":
                _expression = 8;
                break;

            case "Evil":
                _expression = 9;
                break;

            case "Scared":
                _expression = 10;
                break;

            case "HairlessNeutral":
                _expression = 11;
                break;

            case "HairlessAngry":
                _expression = 12;
                break;
        }

        return _expression;
    }
}
