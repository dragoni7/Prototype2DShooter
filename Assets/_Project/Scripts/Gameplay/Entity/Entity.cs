using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public abstract class Entity : MonoBehaviour
    {
        // entity's rigidbody
        public Rigidbody2D rb;

        [SerializeField]
        // entity's healthbar
        protected HealthBar _healthBar;
        public HealthBar HealthBar => _healthBar;

        // abilities able to be performed by the entity
        public List<AbstractAbility> Abilities { get; set; }

        // current attributes, modified
        [SerializeField]
        protected Attributes _currentAttributes;
        public Attributes CurrentAttributes => _currentAttributes;

        // base attributes, not modified
        protected Attributes _baseAttributes;
        public Attributes BaseAttributes => _baseAttributes;
        public float CurrentHealth
        {
            get { return _currentAttributes.Get(AttributeType.Health); }
            set
            {
                // if 0 or less, die
                if (value <= 0)
                {
                    _currentAttributes.Set(AttributeType.Health, value);
                    GameEventManager.Instance.EventBus.Raise(new EntityHealthChangedEvent { entity = this });
                    Die();
                }
                // do not set value above max health
                if (value < _baseAttributes.Get(AttributeType.Health))
                {
                    _currentAttributes.Set(AttributeType.Health, value);
                    GameEventManager.Instance.EventBus.Raise(new EntityHealthChangedEvent { entity = this });
                }
            }
        }

        public float MaxHealth
        {
            get { return _baseAttributes.Get(AttributeType.Health); }
            set
            {
                if (value > 0)
                {
                    _baseAttributes.Set(AttributeType.Health, value);
                }
            }
        }

        public float CurrentSpeed
        {
            get { return _currentAttributes.Get(AttributeType.Speed); }
            set { _currentAttributes.Set(AttributeType.Speed, value); }
        }

        public float BaseSpeed
        {
            get { return _baseAttributes.Get(AttributeType.Speed); }
            set { _baseAttributes.Set(AttributeType.Speed, value); }
        }

        [SerializeField]
        // equipped objects are parented to this
        private Transform _equipParent;
        public Transform EquipParent => _equipParent;

        // if the entity can move
        public bool CanMove { get; set; }

        // if the entity can attack
        public bool CanAttack { get; set; }
        public void SetAttributes(Attributes attributes)
        {
            _currentAttributes = attributes;
            _baseAttributes = (Attributes)attributes.Clone();

            if (HealthBar != null)
            {
                float baseHealth = attributes.Get(AttributeType.Health);
                HealthBar.SetMaxHealth(baseHealth);
                HealthBar.SetHealth(baseHealth);
            }
        }

        // perform's the entity's attack
        public abstract void PerformAttack();

        // increases the entity's health
        public abstract void Heal(float amount);

        // reduces the entity's health
        public abstract void TakeDamage(float damage);

        // kills the entity
        public virtual void Die()
        {
            GameEventManager.Instance.EventBus.Raise(new EntityDeathEvent { entity = this });
        }
    }
}
