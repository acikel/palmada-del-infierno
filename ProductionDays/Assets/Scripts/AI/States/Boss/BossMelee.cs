﻿
using System.Collections;
using UnityEngine;

public class BossMelee : State
{
    private GameObject bossFist;
    private Vector3 bossFistPosition;
    private CameraManager cameraManager;
    
    public override void OnStart()
    {
        cameraManager = InstanceRepository.Instance.Get<CameraManager>();
        
        bossFist = GameObject.transform.Find("BossFist").gameObject;
        bossFistPosition = bossFist.transform.position;
        
        StartCoroutine(Melee());
    }
    
    private IEnumerator Melee()
    {
        bossFist.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        Vector3 goalPos = bossFistPosition + (Vector3.down * 3);
        AudioManager.Instance.PlayOneShot(AudioEvent.Combat.BossStomp, GameObject.transform.position);

        while (Vector3.Distance(bossFist.transform.position, goalPos) > 0.1f)
        {
            bossFist.transform.position =
                Vector3.Lerp(bossFist.transform.position, goalPos, Time.deltaTime * 20);

            yield return null;
        }

        cameraManager.ScreenShake(0.3f);
        yield return new WaitForSeconds(0.5f);
        bossFist.SetActive(false);
        bossFist.transform.position = bossFistPosition;
        
        SetState(new BossDecision());
    }
}
