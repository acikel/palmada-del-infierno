
using System.Collections;
using UnityEngine;

public class BossRanged : State
{
    private GameObject target;
    private Transform targetTransform;
    private PlayerController playerController;

    private int attackAmount = 3;
    private GameObject attackObject;

    private CameraManager cameraManager;
    
    public override void OnStart()
    {
        cameraManager = InstanceRepository.Instance.Get<CameraManager>();
        
        attackAmount = BossConfig.RangedAttackAmount;
        attackObject = BossConfig.RangedAttackPrefab;
        
        target = Blackboard.Get<GameObject>(BlackboardConstants.VARIABLE_TARGET);
        targetTransform = target.transform;
        playerController = target.GetComponent<PlayerController>();

        StartCoroutine(RangedAttack());
    }



    private IEnumerator RangedAttack()
    {
        Vector3 attackPosition;

        for (int i = 0; i < attackAmount; i++)
        {
            attackPosition = playerController.GetFloorPosition();
            yield return new WaitForSeconds(0.25f);
            
            AudioManager.Instance.PlayOneShot(BossConfig.BossAttackSound, attackPosition);
            GameObject attack = GameObject.Instantiate(attackObject, attackPosition, Quaternion.identity);
            attack.GetComponent<BossRayAttack>().BossConfig = BossConfig;
            
            cameraManager.ScreenShake(0.5f);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(2f);
        SetState(new BossDecision());
    }
}
