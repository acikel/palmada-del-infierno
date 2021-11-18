
using System.Collections;
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
    private Coroutine attackCoroutine;
    private bool rightHand = false;
    
    private Animator animator;
    public Animator Animator
    {
        get
        {
            animator ??= GetComponent<Animator>();
            return animator;
        }
    }
    
    
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
        playerController = InstanceRepository.Instance.Get<PlayerController>();

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
            attackCoroutine = StartCoroutine(Attack());
        }

        if (Vector3.Distance(targetTransform.position, GameObject.transform.position) > attackRange)
        {
            SetState(new EngagePlayer());
        }
    }

    private IEnumerator Attack()
    {
        if (Animator != null)
            Animator.SetTrigger("Punch");
        yield return new WaitForSeconds(0.2f);
        
        AudioManager.Instance.PlayOneShot(AudioEvent.Combat.EnemyAttack, GameObject.transform.position);
        playerController.PlayerHit(attackDamage, this.GameObject.transform);

        rightHand = !rightHand;
        if (Animator != null)
            Animator.SetBool("RightHand", rightHand);
        attackCoroutine = null;
    }

    public override void OnEnd()
    {
        if (enemyHP != null)
            enemyHP.EnemyHit -= OnEnemyGotHit;

        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
    }
}
