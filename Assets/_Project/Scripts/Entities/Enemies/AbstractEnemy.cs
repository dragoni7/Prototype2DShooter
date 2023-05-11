using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static dragoni7.ScriptableEntity;

namespace dragoni7
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        public EntityStats Stats { get; private set; }
        public BaseEmitter Emitter { get; set; }
        public void SetStats(EntityStats stats)
        {
            Stats = stats;
        }
    }
}
