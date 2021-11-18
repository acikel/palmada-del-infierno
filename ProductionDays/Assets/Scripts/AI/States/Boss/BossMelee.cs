
using System.Collections;
using DG.Tweening;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BossMelee : State
{
    private GameObject bossFist;
    private Vector3 bossFistPosition;
    private CameraManager cameraManager;
    private Vector3 bossFistGroundPosition;
    private GameObject effect;
    private float attackGroundY;
    
    public override void OnStart()
    {
        cameraManager = InstanceRepository.Instance.Get<CameraManager>();
        
        bossFist = GameObject.transform.Find("BossFist").gameObject;
        bossFistPosition = bossFist.transform.position;


        float floorY = InstanceRepository.Instance.Get<PlayerController>().GetFloorPosition().y;
        Debug.Log(floorY);
        // var renderer = GameObject.GetComponent<Renderer>();
        // if (renderer == null)
        //     renderer = GameObject.GetComponentInChildren<Renderer>();
        //     
        // float bossHalfHeight = renderer.bounds.extents.y;
        bossFistGroundPosition = bossFistPosition;
        bossFistGroundPosition.y = floorY + 0.1f;

        attackGroundY = floorY + BossConfig.MeleeHitHeight;
        
        StartCoroutine(Melee());
    }
    
    private IEnumerator Melee()
    {
        effect = GameObject.Instantiate(BossConfig.MeleeEffect, bossFistGroundPosition, Quaternion.identity);
        
        
        yield return new WaitForSeconds(2.5f);
        bossFist.SetActive(true);
        yield return new WaitForSeconds(.6f);

        // Vector3 goalPos = bossFistPosition + (Vector3.down * BossConfig.MeleeHitHeight);
        AudioManager.Instance.PlayOneShot(AudioEvent.Combat.BossStomp, GameObject.transform.position);

        bossFist.transform.DOMoveY(attackGroundY, 0.25f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(0.25f);
        // while (Vector3.Distance(bossFist.transform.position, goalPos) > 0.1f)
        // {
        //     bossFist.transform.position =
        //         Vector3.Lerp(bossFist.transform.position, goalPos, Time.deltaTime * 20);
        //
        //     yield return null;
        // }

        cameraManager.ScreenShake(0.3f);
        yield return new WaitForSeconds(2f);
        bossFist.SetActive(false);
        bossFist.transform.position = bossFistPosition;
        
        GameObject.Destroy(effect);
        SetState(new BossDecision());
    }

    public override void OnEnd()
    {
        if (effect != null)
            GameObject.Destroy(effect);
    }
}
