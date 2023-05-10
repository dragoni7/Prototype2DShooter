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
        protected Vector3 lookDirection;
        protected float angle;
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
            Vector3 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            lookDirection = mousePosition - transform.position;
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public virtual void FixedUpdate()
        {
            
        }
    }
}
