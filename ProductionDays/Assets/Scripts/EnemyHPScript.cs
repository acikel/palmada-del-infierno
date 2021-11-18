using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{
    [SerializeField] private float _healthpoint;

    private ParticleEffects ParticleEffects { get; set; }
    
    private void Start()
    {
        ParticleEffects = InstanceRepository.Instance.Get<ParticleEffects>();
    }

    public void Updatehealt(float damage)
    {
        _healthpoint -= damage;
        if (_healthpoint <= 0)
        {
            ParticleEffects.SpawnEffect(Effect.Fighting, transform.position);
            Destroy(this.gameObject);
        }
    }
}
