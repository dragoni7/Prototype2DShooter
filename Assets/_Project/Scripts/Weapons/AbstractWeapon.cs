using UnityEngine;
using UnityEngine.InputSystem;

namespace dragoni7
{
    public abstract class AbstractWeapon : MonoBehaviour
    {
        public BaseEmitter Emitter { get; set; }

        [SerializeField] protected Transform emitPoint;

        public bool canMove;
        public abstract void PerformAttack();

        public void Start()
        {
            Emitter.transform.SetParent(transform.GetChild(0));
            Emitter.transform.localPosition = Vector3.zero;
            Emitter.transform.localRotation = Quaternion.Euler(0,0,-90);
        }
    }
}
