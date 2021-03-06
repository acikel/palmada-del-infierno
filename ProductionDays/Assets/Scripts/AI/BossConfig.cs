using FMODUnity;
using UnityEngine;

namespace AI
{
    public class BossConfig : EnemyConfig
    {
        [Header("Boss")] 
        [SerializeField] private float decisionTime = 2f;
        [SerializeField] private EventReference bossAttackSound;
        [SerializeField] private EventReference bossVoice;
        
        [Header("Attack: Melee")] 
        [SerializeField] private float meleeDamage = 1;
        [SerializeField] private float meleeDistance = 6f;
        [SerializeField] private GameObject meleeEffect;
        [SerializeField] private float meleeHitHeight = 2f;

        
        [Header("Attack: Spawn Minion")]
        [SerializeField] private GameObject[] minions;
        [SerializeField] private int minionSpawnAmount = 3;
        [SerializeField] private float spawnWaitDuration = 10;
        [SerializeField] private float spawnDistanceFromCenter = 16;

        [Header("Attack: Ranged")] 
        [SerializeField] private GameObject rangedAttackPrefab;
        [SerializeField] private int rangedAttackAmount = 3;
        [SerializeField] private float rangedAttackDamage = 1;
        [SerializeField] private float rangedAttackLifetime = 1.5f;


        public float MeleeDamage => meleeDamage;
        public GameObject[] Minions => minions;
        public int MinionSpawnAmount => minionSpawnAmount;
        public float SpawnWaitDuration => spawnWaitDuration;
        public float SpawnDistanceFromCenter => spawnDistanceFromCenter;
        public float DecisionTime => decisionTime;
        public float MeleeDistance => meleeDistance;
        public GameObject MeleeEffect => meleeEffect;
        public float MeleeHitHeight => meleeHitHeight;

        public EventReference BossAttackSound => bossAttackSound;
        public EventReference BossVoice => bossVoice;

        public GameObject RangedAttackPrefab => rangedAttackPrefab;
        public int RangedAttackAmount => rangedAttackAmount;
        public float RangedAttackDamage => rangedAttackDamage;
        public float RangedAttackLifetime => rangedAttackLifetime;

        private void OnDestroy()
        {
            AudioManager.Instance.ChangeGameMusic(GameMusic.Fight);
            //InstanceRepository.Instance.Get<LevelManager>().Rooms[InstanceRepository.Instance.Get<LevelManager>().currentRoom].GetComponent<Room>().EnemyCount--;
        }
    }
}