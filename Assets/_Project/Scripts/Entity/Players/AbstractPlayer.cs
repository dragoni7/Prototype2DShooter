using UnityEngine;
using static dragoni7.ScriptableEntity;

namespace dragoni7
{
    public abstract class AbstractPlayer : BaseEntity, IDestructable
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

        [SerializeField]
        private EntityStats _stats;
        public EntityStats Stats => _stats;

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
            _stats = stats;
        }

        public void TakeDamage(int damage)
        {
            if (gameObject.activeSelf)
            {
                _stats.health -= 1;

                if (_stats.health <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
