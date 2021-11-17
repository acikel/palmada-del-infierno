using UnityEngine;

namespace AI
{
    public class MinionConfig : EnemyConfig
    {
        [Header("Minion")] 
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private float attackInterval = 1.5f;
        [SerializeField] private float attackDamage = 1f;

        public float AttackRange => attackRange;
        public float AttackInterval => attackInterval;
        public float AttackDamage => attackDamage;
    }
}