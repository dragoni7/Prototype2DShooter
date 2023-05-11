using UnityEngine;
using UnityEngine.InputSystem;
using static dragoni7.ScriptableEntity;

namespace dragoni7
{
    public abstract class AbstractPlayer : BaseEntity
    {
        public Vector2 EquipPos { get; set; }
        public AbstractWeapon Weapon { get; set; }
        public EntityStats Stats { get; private set; }

        protected float currentSpeed;

        private Camera cam;
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

        protected virtual void Start()
        {
            cam = FindAnyObjectByType<Camera>();

            CurrentSpeed = Stats.speed;
            canMove = true;
            canAttack = true;

            Weapon.transform.position = (Vector2)transform.position + EquipPos;
        }

        public override void Move(Vector3 moveThisFrame)
        {
            MoveThisFrame = moveThisFrame;
            // Move player
            transform.position += moveThisFrame;

            // Move weapon
            Vector3 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 lookDirection = mousePosition - Weapon.transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            Weapon.UpdatePosition(transform.position + (Vector3)EquipPos, Quaternion.Euler(0, 0, angle));
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
