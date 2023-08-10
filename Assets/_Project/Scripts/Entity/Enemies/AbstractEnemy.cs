using System.Collections.Generic;
using UnityEngine;
using static dragoni7.ScriptableEntity;

namespace dragoni7
{
    public abstract class AbstractEnemy : BaseEntity, IDestructable
    {
        [SerializeField]
        private EntityStats _stats;
        public EntityStats Stats => _stats;
        public BaseEmitter Emitter { get; set; }
        public AbstractBrain Brain { get; set; }
        public void Start()
        {
            Brain.AIData.OnAttack += PerformAttack;
            Brain.AIData.OnMove += Move;
        }
        public virtual void PerformAttack(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Emitter.transform.rotation = Quaternion.Euler(0, 0, angle);
            Emitter.TryEmitBullets();
        }
        public void SetStats(EntityStats stats)
        {
            _stats = stats;
        }

        public override void Move(Vector3 moveThisFrame)
        {
            rb.velocity = moveThisFrame * Stats.speed;
        }

        public void TakeDamage(int damage)
        {
            if (gameObject.activeSelf)
            {
                _stats.health -= damage;
                
                if (_stats.health <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}