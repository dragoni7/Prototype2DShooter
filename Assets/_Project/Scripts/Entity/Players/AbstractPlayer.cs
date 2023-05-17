using UnityEngine;
using UnityEngine.InputSystem;
using static dragoni7.ScriptableEntity;

namespace dragoni7
{
    public abstract class AbstractPlayer : BaseEntity
    {
        private AbstractWeapon weapon;
        public AbstractWeapon Weapon
        {
            get
            {
                return weapon;
            }

            set
            {
                weapon = value;
                weapon.transform.SetParent(EquipParent);
                weapon.transform.localPosition = Vector3.zero;
            }
        }
        public EntityStats Stats { get; private set; }

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
            CurrentSpeed = Stats.speed;
            canMove = true;
            canAttack = true;
        }

        public virtual void ToggleWeaponVisible(bool value)
        {
            Weapon.gameObject.SetActive(value);
        }

        public void SetStats(EntityStats stats)
        {
            Stats = stats;
        }
    }
}
