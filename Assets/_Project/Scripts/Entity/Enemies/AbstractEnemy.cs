using UnityEngine;
using static dragoni7.ScriptableEntity;

namespace dragoni7
{
    public abstract class AbstractEnemy : BaseEntity
    {
        public EntityStats Stats { get; private set; }
        public BaseEmitter Emitter { get; set; }
        public AbstractBrain EnemyAI { get; set; }
        public void Start()
        {
            //EnemyAI.OnAttack += PerformAttack;
            //EnemyAI.OnMove += Move;
        }
        public virtual void PerformAttack(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Emitter.transform.rotation = Quaternion.Euler(0, 0, angle);
            Emitter.TryEmitBullets();
        }
        public void SetStats(EntityStats stats)
        {
            Stats = stats;
        }

        public override void Move(Vector3 moveThisFrame)
        {
            rb.velocity = moveThisFrame * Stats.speed;
        }
    }
}