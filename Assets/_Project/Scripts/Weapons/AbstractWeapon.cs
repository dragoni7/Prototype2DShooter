using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static dragoni7.ScriptableWeapon;

namespace dragoni7
{
    public abstract class AbstractWeapon : MonoBehaviour
    {
        public BaseEmitter Emitter { get; set; }

        [SerializeField] protected Transform emitPoint;
        protected Vector3 lookDirection;
        protected float angle;
        private Camera cam;

        public virtual void Start()
        {
            cam = FindAnyObjectByType<Camera>();
        }

        public abstract void PerformAttack();
        public virtual void Update()
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            lookDirection = mousePosition - transform.position;
            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;
            Emitter.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public virtual void FixedUpdate()
        {
            
        }
    }
}
