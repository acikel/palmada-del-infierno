
using UnityEngine;

public class EngagePlayer : State
{
    private GameObject target;
    private Transform targetTransform;

    private AIMovement movement;
    
    public override void OnStart()
    {
        Debug.Log("Engage");
        target = Blackboard.Get<GameObject>(BlackboardConstants.VARIABLE_TARGET);
        targetTransform = target.transform;

        movement = GetComponent<AIMovement>();
    }

    public override void Update()
    {
        if (movement.WalkTowards(targetTransform.position))
        {
            SetState(new AttackPlayer());
        }
    }
}
