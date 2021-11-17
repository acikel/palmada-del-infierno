
using System.Collections;
using UnityEngine;

public class BossSpawnMinions : State
{
    private GameObject target;

    private float spawnDistanceToCenter = 16f;
    private int minionSpawnAmount = 3;
    private float waitingTime = 10f;
    private GameObject minion;
    
    public override void OnStart()
    {
        Debug.Log("Attack");
        target = Blackboard.Get<GameObject>(BlackboardConstants.VARIABLE_TARGET);


        minionSpawnAmount = BossConfig.MinionSpawnAmount;
        waitingTime = BossConfig.SpawnWaitDuration;
        spawnDistanceToCenter = BossConfig.SpawnDistanceFromCenter;
        minion = BossConfig.Minion;

        StartCoroutine(SpawnMinions());
    }
    

    private IEnumerator SpawnMinions()
    {
        Vector3 spawnCenter = Camera.main.transform.position;
        spawnCenter.z = GameObject.transform.position.z;
        
        for (int i = 0; i < minionSpawnAmount; i++)
        {
            Vector3 spawnPosition = spawnCenter;
            
            float random = Random.Range(-1, 1);
            if (random < 0)
                spawnPosition.x -= spawnDistanceToCenter;
            else
                spawnPosition.x += spawnDistanceToCenter;

            var instancedMinion = GameObject.Instantiate(minion, spawnPosition, Quaternion.identity);
            instancedMinion.GetComponent<StateMachine>().SetState(new EngagePlayer());
        }
        

        yield return new WaitForSeconds(waitingTime);
        
        SetState(new BossDecision());
    }
}
