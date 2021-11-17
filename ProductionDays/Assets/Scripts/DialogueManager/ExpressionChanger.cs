using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ExpressionsList
{
    public Sprite Neutral;
    public Sprite Sad;
    public Sprite Happy;
    public Sprite Angry;
    public Sprite Drunk;
    public Sprite Win;
    public Sprite Loss;
    public Sprite Shy;
    public Sprite OnPhone;
    public Sprite Evil;
    public Sprite Scared;
    public Sprite HairlessNeutral;
    public Sprite HairlessAngry;

    public ExpressionsList(Sprite _Neutral, Sprite _Sad, Sprite _Happy, Sprite _Angry, Sprite _Drunk, Sprite _Win, Sprite _Loss, Sprite _Shy, Sprite _OnPhone, Sprite _Evil, Sprite _Scared, Sprite _HairlessNeutral, Sprite _HairlessAngry)
    {
        Neutral = _Neutral;
        Sad = _Sad;
        Happy = _Happy;
        Angry = _Angry;
        Drunk = _Drunk;
        Win = _Win;
        Loss = _Loss;
        Shy = _Shy;
        OnPhone = _OnPhone;
        Evil = _Evil;
        Scared = _Scared;
        HairlessNeutral = _HairlessNeutral;
        HairlessAngry = _HairlessAngry;
    }
}
[System.Serializable]
public class Expression
{
    public enum ExpressionName
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
    public ExpressionName name;
    public Sprite ExpressionSprite;

    public Expression(ExpressionName _name, Sprite _ExpressionSpirite)
    {
        name = _name;
        ExpressionSprite = _ExpressionSpirite;
    }
}

public class ExpressionChanger : MonoBehaviour
{
    [SerializeField] private List<Expression> ExpressionsHelvezia = new List<Expression>();
    [SerializeField] private List<Expression> ExpressionsSike = new List<Expression>();
    [SerializeField] private List<Expression> ExpressionsBhomas = new List<Expression>();
    [SerializeField] private List<Expression> ExpressionsDobo = new List<Expression>();
    [SerializeField] private List<Expression> Phone = new List<Expression>();
    [SerializeField] private List<Expression> Angel = new List<Expression>();

    [SerializeField] private Image ExpressionLeft;
    [SerializeField] private Image ExpressionRight;

    private Dictionary<string, Sprite> HelveziaDict = new Dictionary<string, Sprite>();

    private Dictionary<string, Dictionary<string, Sprite>> PeopleDict = new Dictionary<string, Dictionary<string, Sprite>>();

    private Dictionary<string, Sprite> SikeDict = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> BhomasDict = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> DoboDict = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> PhoneDict = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> AngelDict = new Dictionary<string, Sprite>();

    enum ExpressionName
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
        StoreExpressions();
        StoreDictionaries();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeExpressionLeft(string _mood)
    {
        //ExpressionLeft.sprite = ExpressionsHelvezia[GetExpression(_mood)];
        Sprite temp = null;
        if(HelveziaDict.TryGetValue(_mood, out temp))
        {
            ExpressionLeft.sprite = temp;
        }   
    }

    public void ChangeExpressionRight(string _mood, string _person)
    {
        //ExpressionRight.sprite = Expressions[GetExpression(_mood)];
        Dictionary<string, Sprite> temp;
        Sprite temp2;

        if (PeopleDict.TryGetValue(_person, out temp))
        {
            if(temp.TryGetValue(_mood, out temp2))
            {
                ExpressionRight.sprite = temp2;
            }
        }
    }

    private void StoreExpressions()
    {
        foreach (Expression _expression in ExpressionsHelvezia)
        {
            HelveziaDict.Add(Enum.GetName(typeof(ExpressionName), _expression.name), _expression.ExpressionSprite);
        }
        foreach (Expression _expression in ExpressionsSike)
        {
            SikeDict.Add(Enum.GetName(typeof(ExpressionName), _expression.name), _expression.ExpressionSprite);
        }
        foreach (Expression _expression in ExpressionsBhomas)
        {
            BhomasDict.Add(Enum.GetName(typeof(ExpressionName), _expression.name), _expression.ExpressionSprite);
        }
        foreach (Expression _expression in ExpressionsDobo)
        {
            DoboDict.Add(Enum.GetName(typeof(ExpressionName), _expression.name), _expression.ExpressionSprite);
        }
        foreach (Expression _expression in Phone)
        {
            PhoneDict.Add(Enum.GetName(typeof(ExpressionName), _expression.name), _expression.ExpressionSprite);
        }
        foreach (Expression _expression in Angel)
        {
            AngelDict.Add(Enum.GetName(typeof(ExpressionName), _expression.name), _expression.ExpressionSprite);
        }
    }

    private void StoreDictionaries()
    {
        PeopleDict.Add("Bhomas", BhomasDict);
        PeopleDict.Add("Angel", AngelDict);
        PeopleDict.Add("Sike", SikeDict);
        PeopleDict.Add("Dobo", DoboDict);
        PeopleDict.Add("Phone", PhoneDict);
    }
}
