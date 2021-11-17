using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class BossMeleeIndicator : MonoBehaviour
{
    private BossConfig bossConfig;
    
    private void Start()
    {
        bossConfig = transform.parent.GetComponent<BossConfig>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.PlayerHit(bossConfig.MeleeDamage, transform);
        }
    }
}
