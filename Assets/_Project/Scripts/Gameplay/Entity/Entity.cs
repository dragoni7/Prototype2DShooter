using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public abstract class Entity : MonoBehaviour
    {
        public Rigidbody2D rb;

        [SerializeField]
        protected HealthBar _healthBar;
        public HealthBar HealthBar => _healthBar;
        public List<AbstractAbility> Abilities { get; set; }

        protected EntityAttributes _attributes;
        public EntityAttributes Attributes => _attributes;

        [SerializeField]
        private Transform _equipParent;
        public Transform EquipParent => _equipParent;
        public bool CanMove { get; set; }
        public bool CanAttack { get; set; }
        public void SetAttributes(EntityAttributes stats)
        {
            _attributes = stats;

            if (HealthBar != null)
            {
                HealthBar.SetMaxHealth(stats.health);
                HealthBar.SetHealth(stats.health);
            }
        }
        public abstract void PerformAttack();
        public abstract void TakeDamage(float damage);
        public virtual void Die()
        {
            EventSystem.Instance.TriggerEvent(Events.OnEntityDie, new Dictionary<string, object> { { "Entity", this } });
        }
    }
}
