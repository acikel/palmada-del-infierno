﻿
using System.Collections;
using AI;
using UnityEngine;

public class BossSpawnMinions : State
{
    private GameObject target;

    private float spawnDistanceToCenter = 16f;
    private int minionSpawnAmount = 3;
    private float waitingTime = 10f;
    private GameObject minion;

    private CameraManager cameraManager;
    
    public override void OnStart()
    {
        cameraManager = InstanceRepository.Instance.Get<CameraManager>();
        
        target = Blackboard.Get<GameObject>(BlackboardConstants.VARIABLE_TARGET);


        minionSpawnAmount = BossConfig.MinionSpawnAmount;
        waitingTime = BossConfig.SpawnWaitDuration;
        spawnDistanceToCenter = BossConfig.SpawnDistanceFromCenter;
        minion = BossConfig.Minion;

        StartCoroutine(SpawnMinions());
    }
    

    private IEnumerator SpawnMinions()
    {
        Vector3 spawnCenter = cameraManager.transform.position;
        spawnCenter.z = GameObject.transform.position.z;
        
        cameraManager.ScreenShake(2f, 0.1f);
        
        for (int i = 0; i < minionSpawnAmount; i++)
        {
            Vector3 spawnPosition = spawnCenter;
            
            float random = Random.Range(-1f, 1f);
            if (random < 0)
                spawnPosition.x -= spawnDistanceToCenter;
            else
                spawnPosition.x += spawnDistanceToCenter;

            var instancedMinion = GameObject.Instantiate(minion, spawnPosition, Quaternion.identity);
            instancedMinion.GetComponent<MinionConfig>().BossSpawned = true;
            instancedMinion.GetComponent<StateMachine>().SetState(new EngagePlayer());
        }
        

        yield return new WaitForSeconds(waitingTime);
        
        SetState(new BossDecision());
    }
}
