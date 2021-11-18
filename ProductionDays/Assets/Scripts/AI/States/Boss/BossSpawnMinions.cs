
using System.Collections;
using AI;
using UnityEngine;

public class BossSpawnMinions : State
{
    private GameObject target;

    private float spawnDistanceToCenter = 16f;
    private int minionSpawnAmount = 3;
    private float waitingTime = 10f;
    private GameObject[] minions;
    private float floorY = 0f;

    private CameraManager cameraManager;
    
    public override void OnStart()
    {
        cameraManager = InstanceRepository.Instance.Get<CameraManager>();
        
        target = Blackboard.Get<GameObject>(BlackboardConstants.VARIABLE_TARGET);
        floorY = target.GetComponent<PlayerController>().GetFloorPosition().y;
        Debug.Log(floorY);
        
        minionSpawnAmount = BossConfig.MinionSpawnAmount;
        waitingTime = BossConfig.SpawnWaitDuration;
        spawnDistanceToCenter = BossConfig.SpawnDistanceFromCenter;
        minions = BossConfig.Minions;

        StartCoroutine(SpawnMinions());
    }
    

    private IEnumerator SpawnMinions()
    {
        Vector3 spawnCenter = cameraManager.transform.position;
        spawnCenter.y = floorY + 1f;
        spawnCenter.z = GameObject.transform.position.z;
        
        cameraManager.ScreenShake(2f, 0.1f);
        
        for (int i = 0; i < minionSpawnAmount; i++)
        {
            Vector3 spawnPosition = spawnCenter;
            
            float random = Random.Range(-1f, 1f);
            if (random < 0)
                spawnPosition.x -= spawnDistanceToCenter + i * 1.5f;
            else
                spawnPosition.x += spawnDistanceToCenter + i * 1.5f;

            var instancedMinion = GameObject.Instantiate(minions[Random.Range(0, minions.Length)], spawnPosition, Quaternion.Euler(0, -90, 0));
            instancedMinion.GetComponent<MinionConfig>().BossSpawned = true;
            instancedMinion.GetComponent<StateMachine>().SetState(new EngagePlayer());
        }
        

        yield return new WaitForSeconds(waitingTime);
        
        SetState(new BossDecision());
    }
}
