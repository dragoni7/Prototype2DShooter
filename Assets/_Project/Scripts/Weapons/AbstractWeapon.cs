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

        public abstract void PerformAttack();

        public virtual void UpdatePosition(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;

            Vector3 emitterAngles = rotation.eulerAngles;

            Emitter.UpdatePosition(emitPoint.position, Quaternion.Euler(emitterAngles.x, emitterAngles.y, emitterAngles.z - 90));
        }
        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }
    }
}
