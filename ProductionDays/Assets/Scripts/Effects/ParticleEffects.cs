using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Effect
{
    Damage,
    Fighting,
    Health,
    Hit,
    Miss
}

[DefaultExecutionOrder(0)]
public class ParticleEffects : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject damage;
    [SerializeField] private GameObject fighting;
    [SerializeField] private GameObject health;
    [SerializeField] private GameObject hit;
    [SerializeField] private GameObject miss;
    
    void Awake()
    {
        InstanceRepository.Instance.Add(this);
    }

    public void SpawnEffect(Effect effect, Vector3 position)
    {
        GameObject effectPrefab = null;
        switch (effect)
        {
            case Effect.Damage:
                effectPrefab = damage;
                break;
            case Effect.Fighting:
                effectPrefab = fighting;
                break;
            case Effect.Health:
                effectPrefab = health;
                break;
            case Effect.Hit:
                effectPrefab = hit;
                break;
            case Effect.Miss:
                effectPrefab = miss;
                break;
        }

        if (effectPrefab != null)
            Instantiate(effectPrefab, position, Quaternion.identity);
    }

    public void SpawnEffects(Effect[] effects, Vector3 position)
    {
        foreach (Effect effect in effects)
        {
            SpawnEffect(effect, position);
        }
    }

    private void OnDestroy()
    {
        InstanceRepository.Instance.Remove(this);
    }
}
