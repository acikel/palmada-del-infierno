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
    
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        
        InvokeRepeating("CheckForSpawn",  0f,0.15f);
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnDelay);
        spawnedEnemy = Instantiate(enemy, transform.position, transform.rotation);
    }

    void CheckForSpawn()
    {
        if (spawnedEnemy == null)
        {
            StartCoroutine(SpawnEnemy());
        }
    }
}
