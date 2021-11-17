using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int EnemyCount;
    public float xScale { get; private set; }
    public float zScale { get; private set; }

    public bool isBossRoom = false;

    public GameObject Boss;
    public Transform SpawnTarget;

    void Awake()
    {
        xScale = transform.GetChild(0).transform.localScale.x;
        zScale = transform.GetChild(0).transform.localScale.z;
    }

    public void SpawnBoss()
    {
        if (isBossRoom)
        {
            Instantiate(Boss, SpawnTarget);
        }
    }
}
