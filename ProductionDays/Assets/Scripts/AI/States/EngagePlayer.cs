
using AI;
using UnityEngine;

public class EngagePlayer : State
{
    private GameObject target;
    private Transform targetTransform;

    private AIMovement movement;
    private float attackRange;

    public override void OnStart()
    {
        Debug.Log("Engage");

        attackRange = MinionConfig.AttackRange;
        
        target = Blackboard.Get<GameObject>(BlackboardConstants.VARIABLE_TARGET);
        if (target == null)
        {
            target = GameObject.FindWithTag("Player");
        }
        
        targetTransform = target.transform;

        movement = GetComponent<AIMovement>();
    }

    public override void Update()
    {
        if (movement.WalkTowards(targetTransform.position, attackRange))
        {
            SetState(new AttackPlayer());
        }
    }
}
