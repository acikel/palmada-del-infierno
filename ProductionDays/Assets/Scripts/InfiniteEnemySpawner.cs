using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteEnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnDelay = 0.5f;

    [SerializeField]
    private GameObject enemy;

    private GameObject spawnedEnemy;

    private int amountToKill = 2;
    private int enemyCounter = 0;
    
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        
        InvokeRepeating("CheckForSpawn",  0f,0.15f);
    }

    IEnumerator SpawnEnemy()
    {

        spawnedEnemy = Instantiate(enemy, transform.position, transform.rotation);
        yield return new WaitForSeconds(spawnDelay);
    }

    void CheckForSpawn()
    {
        if (spawnedEnemy == null)
        {
            //StartCoroutine(SpawnEnemy());
            //enemyCounter++;
            if (enemyCounter < amountToKill)
            {
                StartCoroutine(SpawnEnemy());
                enemyCounter++;
            }
            else
            {
                InstanceRepository.Instance.Get<LevelManager>().RoomCleared();
            }
            
        }
    }
}
