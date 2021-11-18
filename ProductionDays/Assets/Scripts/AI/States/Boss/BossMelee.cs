
using System.Collections;
using DG.Tweening;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BossMelee : State
{
    private GameObject bossFist;
    private Vector3 bossPosition;
    private CameraManager cameraManager;
    private Vector3 bossFistGroundPosition;
    private GameObject effect;
    private float attackGroundY;
    private Vector3 attackPosition;

    private PlayerController playerController;
    private Renderer[] renderers;
    private bool updateEffectPosition = false;
    
    public override void OnStart()
    {
        cameraManager = InstanceRepository.Instance.Get<CameraManager>();

        bossPosition = GameObject.transform.position;
        bossFist = GameObject.transform.Find("BossFist").gameObject;

        renderers = GameObject.GetComponentsInChildren<Renderer>();

        playerController = InstanceRepository.Instance.Get<PlayerController>();
        float floorY = playerController.GetFloorPosition().y;
        // var renderer = GameObject.GetComponent<Renderer>();
        // if (renderer == null)
        //     renderer = GameObject.GetComponentInChildren<Renderer>();
        //     
        // float bossHalfHeight = renderer.bounds.extents.y;
        bossFistGroundPosition = GameObject.transform.position;
        bossFistGroundPosition.y = floorY + 0.1f;

        attackGroundY = floorY + BossConfig.MeleeHitHeight;
        
        StartCoroutine(Melee());
    }

    public override void Update()
    {
        if (updateEffectPosition)
        {
            effect.transform.position = Vector3.Lerp(effect.transform.position, playerController.transform.position + new Vector3(0, 0.1f, 0), Time.deltaTime * 6f);
        }
    }

    private IEnumerator Melee()
    {
        GameObject.transform.DOMoveY(attackGroundY + 20f, 1f);
        
        effect = GameObject.Instantiate(BossConfig.MeleeEffect, bossFistGroundPosition, Quaternion.identity);
        updateEffectPosition = true;
        
        yield return new WaitForSeconds(2.5f);
        updateEffectPosition = false;
        Vector3 pos = effect.transform.position;
        pos.y = attackGroundY + 20f;
        bossFist.transform.position = pos; 
        EnableRenderers(false);
        yield return new WaitForSeconds(0.05f);
        bossFist.SetActive(true);
        yield return new WaitForSeconds(.6f);

        // Vector3 goalPos = bossFistPosition + (Vector3.down * BossConfig.MeleeHitHeight);
        AudioManager.Instance.PlayOneShot(AudioEvent.Combat.BossStomp, effect.transform.position);

        bossFist.transform.DOMoveY(attackGroundY, 0.25f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(0.25f);

        cameraManager.ScreenShake(0.3f);
        yield return new WaitForSeconds(2f);
        
        bossFist.SetActive(false);
        GameObject.transform.position = bossPosition;

        yield return new WaitForSeconds(0.05f);
        EnableRenderers(true);
        GameObject.Destroy(effect);
        SetState(new BossDecision());
    }

    private void EnableRenderers(bool enabled)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = enabled;
        }
    }

    public override void OnEnd()
    {
        if (effect != null)
            GameObject.Destroy(effect);
    }
}
