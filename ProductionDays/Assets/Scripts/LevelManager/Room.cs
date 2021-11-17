using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int EnemyCount;
    public float xScale { get; private set; }
    public float zScale { get; private set; }

    void Awake()
    {
        xScale = transform.GetChild(0).transform.localScale.x;
        zScale = transform.GetChild(0).transform.localScale.z;
    }
}
