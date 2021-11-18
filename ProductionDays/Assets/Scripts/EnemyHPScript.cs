using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{
    [SerializeField] private float _healthpoint;

    private ParticleEffects ParticleEffects { get; set; }
    private Rigidbody Rigidbody;
    private MinionConfig MinionConfig;
    
    private void Start()
    {
        ParticleEffects = InstanceRepository.Instance.Get<ParticleEffects>();
        Rigidbody = GetComponent<Rigidbody>();
        MinionConfig = GetComponent<MinionConfig>();
    }

    public void ApplyDamage(float damage)
    {
        _healthpoint -= damage;
        if (_healthpoint <= 0)
        {
            ParticleEffects.SpawnEffect(Effect.Fighting, transform.position);
            Destroy(this.gameObject);
        }
    }

    public void KnockBack(Vector3 attackerPosition)
    {
        Vector3 difference = attackerPosition - transform.position;
        Vector3 force = Vector3.right;
        if (difference.x > 0)
            force = Vector3.left;

        force *= MinionConfig.KnockBackStrength;
        
        Rigidbody.AddForce(force, ForceMode.Impulse);
    }
}
