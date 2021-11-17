using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPScript : MonoBehaviour
{
    [SerializeField] private float _healthpoint;

    public void Updatehealt(float damage)
    {
        _healthpoint -= damage;
        if (_healthpoint <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
