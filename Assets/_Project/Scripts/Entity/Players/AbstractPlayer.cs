using UnityEngine;
using UnityEngine.InputSystem;
using static dragoni7.ScriptableEntity;

namespace dragoni7
{
    public abstract class AbstractPlayer : BaseEntity
    {
        public AbstractWeapon Weapon { get; set; }
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
            Weapon.transform.SetParent(transform.GetChild(0));
            Weapon.transform.localPosition = Vector3.zero;

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
