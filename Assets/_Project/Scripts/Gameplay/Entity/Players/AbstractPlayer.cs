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


        public virtual void Start()
        {
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
                CurrentHealth -= damage;
            }
        }

        public override void Heal(float amount)
        {
            CurrentHealth += amount;
        }
        public override void Die()
        {
            base.Die();
            Destroy(gameObject);
        }
    }
}
