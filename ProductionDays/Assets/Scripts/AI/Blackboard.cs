using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    private Dictionary<string, object> variables;
    private StateMachine owner;

    public Blackboard(StateMachine owner)
    {
        this.owner = owner;
        variables = new Dictionary<string, object>();
    }

    public void Set(string name, object value)
    {
        if (!variables.ContainsKey(name))
            variables.Add(name, value);
        else
            variables[name] = value;
    }

    public object Get(string name)
    {
        if (!variables.ContainsKey(name))
            return null;
        else
            return variables[name];
    }
    
    public string GetString(string name)
    {
        if (!variables.ContainsKey(name))
            return null;
        else
            return (string)variables[name];
    }
    
    public int GetInt(string name)
    {
        if (!variables.ContainsKey(name))
            return 0;
        else
            return (int)variables[name];
    }
    
    public float GetFloat(string name)
    {
        if (!variables.ContainsKey(name))
            return 0;
        else
            return (float)variables[name];
    }
    
    public T Get<T>(string name)
    {
        if (!variables.ContainsKey(name))
            return default;
        else
            return (T)variables[name];
    }
}