using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractEnemy : Entity
    {
        public BaseEmitter Emitter { get; set; }
        public AbstractBrain Brain { get; set; }
        public void Start()
        {
            CanMove = true;
            CanAttack = true;
            Brain.AIData.entity = this;
        }
    }
}