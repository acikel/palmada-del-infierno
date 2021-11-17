using UnityEngine;

namespace AI
{
    public class BossConfig : EnemyConfig
    {
        [Header("Boss")] 
        [SerializeField] private float decisionTime = 2f;

        [Header("Attack: Melee")] 
        [SerializeField] private float meleeDamage;

        [SerializeField] private float meleeDistance = 6f;
        
        [Header("Attack: Spawn Minion")]
        [SerializeField] private GameObject minion;
        [SerializeField] private int minionSpawnAmount = 3;
        [SerializeField] private float spawnWaitDuration = 10;
        [SerializeField] private float spawnDistanceFromCenter = 16;

        [Header("Attack: Ranged")] [SerializeField]
        private GameObject rangedAttackPrefab;
        [SerializeField] private int rangedAttackAmount = 3;
        [SerializeField] private float rangedAttackDamage = 1;
        [SerializeField] private float rangedAttackLifetime = 1.5f;


        public float MeleeDamage => meleeDamage;
        public GameObject Minion => minion;
        public int MinionSpawnAmount => minionSpawnAmount;
        public float SpawnWaitDuration => spawnWaitDuration;
        public float SpawnDistanceFromCenter => spawnDistanceFromCenter;
        public float DecisionTime => decisionTime;
        public float MeleeDistance => meleeDistance;

        public GameObject RangedAttackPrefab => rangedAttackPrefab;
        public int RangedAttackAmount => rangedAttackAmount;
        public float RangedAttackDamage => rangedAttackDamage;
        public float RangedAttackLifetime => rangedAttackLifetime;
    }
}