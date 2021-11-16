using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTriggerScript : MonoBehaviour
{
    void OnTriggerEnter(Collision col)
    {
        transform.parent.GetComponent<PlayerController>().EnemyHit(col);
    }
}
