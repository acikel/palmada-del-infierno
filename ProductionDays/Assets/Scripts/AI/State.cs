using System.Collections;
using System.Collections.Generic;
using AI;
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

    protected Coroutine StartCoroutine(IEnumerator routine)
    {
        return StateMachine.StateStartCoroutine(routine);
    }

    protected void StopCoroutine(Coroutine coroutine)
    {
        StateMachine.StateStopCoroutine(coroutine);
    }

    protected EnemyConfig Config => StateMachine.EnemyConfig;
    protected BossConfig BossConfig => (BossConfig)StateMachine.EnemyConfig;
    protected MinionConfig MinionConfig => (MinionConfig)StateMachine.EnemyConfig;
    
    public virtual void OnStart() { }
    public virtual void Update() { }
    public virtual void OnEnd() { }
}
