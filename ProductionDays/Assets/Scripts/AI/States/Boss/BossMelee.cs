
using System.Collections;
using UnityEngine;

public class BossMelee : State
{
    private GameObject bossFist;
    private Vector3 bossFistPosition;
    private CameraManager cameraManager;
    private Vector3 bossFistGroundPosition;
    private GameObject effect; 
    
    public override void OnStart()
    {
        cameraManager = InstanceRepository.Instance.Get<CameraManager>();
        
        bossFist = GameObject.transform.Find("BossFist").gameObject;
        bossFistPosition = bossFist.transform.position;

        var renderer = GameObject.GetComponent<Renderer>();
        if (renderer == null)
            renderer = GameObject.GetComponentInChildren<Renderer>();
            
        float bossHalfHeight = renderer.bounds.extents.y;
        bossFistGroundPosition = bossFistPosition;
        bossFistGroundPosition.y = GameObject.transform.position.y - bossHalfHeight + 0.1f;
        
        StartCoroutine(Melee());
    }
    
    private IEnumerator Melee()
    {
        effect = GameObject.Instantiate(BossConfig.MeleeEffect, bossFistGroundPosition, Quaternion.identity);
        
        
        yield return new WaitForSeconds(3f);
        bossFist.SetActive(true);
        yield return new WaitForSeconds(.5f);

        Vector3 goalPos = bossFistPosition + (Vector3.down * 3);
        AudioManager.Instance.PlayOneShot(AudioEvent.Combat.BossStomp, GameObject.transform.position);

        while (Vector3.Distance(bossFist.transform.position, goalPos) > 0.1f)
        {
            bossFist.transform.position =
                Vector3.Lerp(bossFist.transform.position, goalPos, Time.deltaTime * 20);

            yield return null;
        }

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
