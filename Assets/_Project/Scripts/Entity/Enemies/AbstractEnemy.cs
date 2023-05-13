using UnityEngine;
using static dragoni7.ScriptableEntity;

namespace dragoni7
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        public EntityStats Stats { get; private set; }
        public BaseEmitter Emitter { get; set; }
        public virtual void PerformAttack()
        {
            Emitter.TryEmitBullets();
        }
        public void SetStats(EntityStats stats)
        {
            Stats = stats;
        }
    }
}
