using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [TypeDropDown(typeof(State))]
    [SerializeField]
    private string startStateType;

    private State currentState;
    
    public Blackboard Blackboard { get; private set; }
    
    private void Start()
    {
        Blackboard = new Blackboard(this);
        
        Type stateType = Type.GetType(startStateType);
        if (stateType == null)
        {
            Debug.LogError("StartStateType was not found");
            return;
        }

        State startState = (State)Activator.CreateInstance(stateType);
        SetState(startState);
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnEnd();

        state.StateMachine = this;
        state.OnStart();
        currentState = state;
    }

    private void Update()
    {
        if (currentState != null)
            currentState.Update();
    }
}
