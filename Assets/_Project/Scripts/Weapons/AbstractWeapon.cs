using UnityEngine;
using UnityEngine.InputSystem;

namespace dragoni7
{
    public abstract class AbstractWeapon : MonoBehaviour
    {
        public BaseEmitter Emitter { get; set; }

        [SerializeField] private Transform emitPoint;
        public Transform EmitPoint => emitPoint;

        public bool canMove;
        public abstract void PerformAttack();
    }
}
