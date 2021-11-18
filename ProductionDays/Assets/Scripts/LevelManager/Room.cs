using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int EnemyCount;
    public float xScale { get; private set; }
    public float zScale { get; private set; }

    public bool isBossRoom = false;

    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject Enemy;
    private Transform SpawnTarget;

    public int currentEnemyCount;
    private int currentEnemy;
    private List<GameObject> Enemies = new List<GameObject>();

    private bool enemiesSpawned = false;

    void Awake()
    {
        xScale = transform.GetChild(0).transform.localScale.x;
        zScale = transform.GetChild(0).transform.localScale.z;
        currentEnemyCount = EnemyCount;
        SpawnTarget = transform.GetChild(2).transform;
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
            for(int i = 0; i < EnemyCount; i++)
            {
                temp = Instantiate(Enemy, SpawnTarget);
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
