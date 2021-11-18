using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int EnemyCount;
    public float xScale { get; private set; }
    public float zScale { get; private set; }

    public bool isBossRoom = false;
    [SerializeField] private float spawnDistanceFromCenter = 16;

    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject Enemy;
    private Transform SpawnTarget;

    public int currentEnemyCount;
    private int currentEnemy;
    private List<GameObject> Enemies = new List<GameObject>();

    private bool enemiesSpawned = false;
    private CameraManager cameraManager;

    void Awake()
    {
        currentEnemyCount = 1;
        xScale = transform.GetChild(0).transform.localScale.x;
        zScale = transform.GetChild(0).transform.localScale.z;
        SpawnTarget = transform.GetChild(2).transform;
    }

    private void Start()
    {
        cameraManager = InstanceRepository.Instance.Get<CameraManager>();
    }

    void Update()
    {
        if (enemiesSpawned) currentEnemyCount = SpawnTarget.childCount;

    }

    public void SpawnEnemies()
    {
        GameObject temp;
        if (isBossRoom)
        {
            AudioManager.Instance.ChangeGameMusic(GameMusic.Boss);
            Instantiate(Boss, SpawnTarget);
        }
        else
        {
            if (cameraManager == null)
            {
                cameraManager = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraManager>();
            }
            Vector3 spawnCenter = cameraManager.transform.position;
            spawnCenter.z = transform.position.z;
            
            AudioManager.Instance.ChangeGameMusic(GameMusic.Fight);
            for(int i = 0; i < EnemyCount; i++)
            {
                Vector3 spawnPosition = spawnCenter;
                spawnPosition.y = 3f;
                float random = UnityEngine.Random.Range(-1f, 1f);
                if (random < 0)
                    spawnPosition.x -= spawnDistanceFromCenter;
                else
                    spawnPosition.x += spawnDistanceFromCenter;
                
                temp = Instantiate(Enemy, spawnPosition, Quaternion.identity, SpawnTarget);
                temp.GetComponent<StateMachine>().SetState(new EngagePlayer());
                Enemies.Add(temp);
                temp.SetActive(false);
                
            }
            InvokeRepeating("SpawnEnemy", 1f, 2f);
        }
        enemiesSpawned = true;
    }

    private void SpawnEnemy()
    {
        if(currentEnemy < EnemyCount)
        {
            Enemies[currentEnemy].SetActive(true);
            currentEnemy++;
        }
    }
}
