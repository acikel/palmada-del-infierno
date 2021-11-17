
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossDecision : State
{
    private Vector3 position;
    private float sightDistance = 8f;

    private float closeAttackDistance = 4f;
    
    private Transform playerTransform;
    private float decisionTime = 2f;

    private float elapsedDecisionTime = 0f;

    private Vector3 targetPosition;
    
    public override void OnStart()
    {
        Debug.Log("Decide");
        position = GameObject.transform.position;

        decisionTime = BossConfig.DecisionTime;
        closeAttackDistance = BossConfig.MeleeDistance;
        
        var target = Blackboard.Get<GameObject>(BlackboardConstants.VARIABLE_TARGET);
        if (target == null)
        {
            target = GameObject.FindWithTag("Player");
            //target = InstanceRepository.Instance.Get<PlayerController>();
            Blackboard.Set(BlackboardConstants.VARIABLE_TARGET, target);
            
        }

        playerTransform = target.transform;
    }

    public override void Update()
    {
        elapsedDecisionTime += Time.deltaTime;

        targetPosition = new Vector3(position.x, position.y, playerTransform.position.z);
        GameObject.transform.position =
            Vector3.Lerp(GameObject.transform.position, targetPosition, Time.deltaTime * 10);

        if (elapsedDecisionTime > decisionTime)
        {
            List<Type> possibleAttackStates = new List<Type>();
            float distanceToPlayer = Vector3.Distance(GameObject.transform.position, playerTransform.position);

            if (distanceToPlayer < closeAttackDistance)
            {
                possibleAttackStates.Add(typeof(BossMelee));
            }
            
            possibleAttackStates.Add(typeof(BossSpawnMinions));
            possibleAttackStates.Add(typeof(BossRanged));

            if (possibleAttackStates.Count == 0)
            {
                elapsedDecisionTime = 0;
                return;
            }
            
            Type attackState = possibleAttackStates[Random.Range(0, possibleAttackStates.Count)];
            
            elapsedDecisionTime = 0;
            SetState((State)Activator.CreateInstance(attackState));
        }
    }
}
