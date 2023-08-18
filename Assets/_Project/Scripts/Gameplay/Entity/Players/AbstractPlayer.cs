using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractPlayer : Entity
    {
        private AbstractWeapon _weapon;
        public AbstractWeapon Weapon
        {
            get
            {
                return _weapon;
            }

            set
            {
                _weapon = value;
                _weapon.transform.SetParent(EquipParent);
                _weapon.transform.localPosition = Vector3.zero;
            }
        }

        protected float currentSpeed;
        public float CurrentSpeed
        {
            get { return currentSpeed; }
            set
            {
                if (value != currentSpeed)
                {
                    currentSpeed = value;
                }
                return;
            }
        }
        public virtual void Start()
        {
            CurrentSpeed = _attributes.speed;
            CanMove = true;
            CanAttack = true;
        }
        public virtual void ToggleWeaponVisible(bool value)
        {
            Weapon.gameObject.SetActive(value);
        }
        public override void TakeDamage(float damage)
        {
            if (gameObject.activeSelf)
            {
                _attributes.health -= damage;

                if (_attributes.health <= 0)
                {
                    Die();
                }
            }
        }

        public virtual void Heal(float amount)
        {
            _attributes.health += amount;
        }
        public override void Die()
        {
            base.Die();
            Destroy(gameObject);
        }
    }
}
