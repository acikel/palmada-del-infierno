
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

    private EnemyHPScript enemyHP;
    
    public override void OnStart()
    {
        attackRange = MinionConfig.AttackRange;
        attackInterval = MinionConfig.AttackInterval;
        attackDamage = MinionConfig.AttackDamage;
        
        target = Blackboard.Get<GameObject>(BlackboardConstants.VARIABLE_TARGET);

        if (target == null)
        {
            target = InstanceRepository.Instance.Get<PlayerController>().gameObject; //GameObject.FindWithTag("Player");
        }
        
        targetTransform = target.transform;
        playerController = target.GetComponent<PlayerController>();

        enemyHP = GameObject.GetComponent<EnemyHPScript>();
        if (enemyHP != null)
            enemyHP.EnemyHit += OnEnemyGotHit;
        
        attackTime = Time.time - (attackInterval * 0.9f);
    }

    private void OnEnemyGotHit()
    {
        // Reset attack timer
        attackTime = Time.time;
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
        playerController = InstanceRepository.Instance.Get<PlayerController>();
        AudioManager.Instance.PlayOneShot(AudioEvent.Combat.EnemyAttack, GameObject.transform.position);
        playerController.PlayerHit(attackDamage, this.GameObject.transform);
    }

    public override void OnEnd()
    {
        if (enemyHP != null)
            enemyHP.EnemyHit -= OnEnemyGotHit;
    }
}
