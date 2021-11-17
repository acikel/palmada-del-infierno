using UnityEngine;

namespace AI
{
    public class MinionConfig : EnemyConfig
    {
        [Header("Minion")] 
        [SerializeField] private float attackRange = 1.5f;
        [SerializeField] private float attackInterval = 1.5f;
        [SerializeField] private float attackDamage = 1f;
        public bool BossSpawned = false;

        public float AttackRange => attackRange;
        public float AttackInterval => attackInterval;
        public float AttackDamage => attackDamage;

        /*void OnDestroy()
        {
            if (!BossSpawned)
            {
                InstanceRepository.Instance.Get<LevelManager>()
                    .Rooms[InstanceRepository.Instance.Get<LevelManager>().currentRoom].GetComponent<Room>()
                    .EnemyCount--;
            }
        }*/

    }
}