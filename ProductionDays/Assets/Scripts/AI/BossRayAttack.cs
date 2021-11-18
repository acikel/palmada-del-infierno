using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class BossRayAttack : MonoBehaviour
{
    public BossConfig BossConfig { get; set; }

    private void Start()
    {
        StartCoroutine(WaitToDie());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.PlayerHit(BossConfig.RangedAttackDamage, transform);
        }
    }

    private IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(0.25f);
        yield return new WaitForSeconds(BossConfig.RangedAttackLifetime - 0.75f);
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
