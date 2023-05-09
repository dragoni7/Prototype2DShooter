using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static dragoni7.ScriptableWeapon;

namespace dragoni7
{
    public abstract class AbstractWeapon : Entity
    {
        public ScriptableBullet Bullet { get; set; }
        public WeaponStats Stats { get; protected set; }

        [SerializeField] protected Transform bulletSpawnPoint;
        protected int attackCounter;
        protected Vector2 mousePosition;
        private Camera cam;

        public virtual void Start()
        {
            canMove = true;
            canAttack = true;
            cam = FindAnyObjectByType<Camera>();
        }

        public void SetStats(WeaponStats stats)
        {
            Stats = stats;
        }

        public abstract void PerformAttack();
        public virtual void Update()
        {
            mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        public virtual void FixedUpdate()
        {
            Vector2 lookDir = mousePosition - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}
