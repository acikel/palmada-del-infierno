using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [TypeDropDown(typeof(State))]
    [SerializeField]
    private string startStateType;

    private State currentState;
    
    public Blackboard Blackboard { get; private set; }
    public EnemyConfig EnemyConfig { get; private set; }

    private void Awake()
    {
        Blackboard = new Blackboard(this);
        EnemyConfig = GetComponent<EnemyConfig>();
        
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

    public Coroutine StateStartCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void StateStopCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }

    private void Update()
    {
        if (currentState != null)
            currentState.Update();
    }

    private void OnDestroy()
    {
        if (currentState != null)
            currentState.OnEnd();
    }
}
