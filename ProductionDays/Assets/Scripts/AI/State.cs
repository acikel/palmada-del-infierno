using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public StateMachine StateMachine { get; internal set; }
    public Blackboard Blackboard => StateMachine.Blackboard;
    public GameObject GameObject => StateMachine.gameObject;


    protected void SetState(State newState)
    {
        StateMachine.SetState(newState);
    }

    protected T GetComponent<T>()
    {
        return GameObject.GetComponent<T>();
    }
    
    public virtual void OnStart() { }
    public virtual void Update() { }
    public virtual void OnEnd() { }
}
