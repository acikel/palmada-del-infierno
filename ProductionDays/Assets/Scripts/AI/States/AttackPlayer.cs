
using UnityEngine;

public class AttackPlayer : State
{
    private GameObject target;
    private Transform targetTransform;
    private PlayerController playerController;
    
    private float attackInterval = 1f;
    private float attackDamage = 1f;
    private float attackRange = 1.5f;
    private float attackTime = 0f;
    
    public override void OnStart()
    {
        Debug.Log("Attack");

        attackRange = MinionConfig.AttackRange;
        attackInterval = MinionConfig.AttackInterval;
        attackDamage = MinionConfig.AttackDamage;
        
        target = Blackboard.Get<GameObject>(BlackboardConstants.VARIABLE_TARGET);

        if (target == null)
        {
            target = GameObject.FindWithTag("Player");
        }
        
        targetTransform = target.transform;
        playerController = target.GetComponent<PlayerController>();
    }

    public override void Update()
    {
        if (Time.time - attackTime >= attackInterval)
        {
            attackTime = Time.time;
            Attack();
        }

        if (Vector3.Distance(targetTransform.position, GameObject.transform.position) > attackRange)
        {
            SetState(new EngagePlayer());
        }
    }

    private void Attack()
    {
        playerController.PlayerHit(attackDamage, this.GameObject.transform);
    }
}
