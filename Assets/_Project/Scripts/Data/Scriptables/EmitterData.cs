using System;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Emitter")]
    public class EmitterData : AbstractData
    {
        public BaseEmitter emitterPrefab;
        public BulletData scriptableBullet;
        public BasePattern patternPrefab;

        [SerializeField] private EmitterStats stats;
        public EmitterStats BaseStats => stats;

        [Serializable]
        public struct EmitterStats
        {
            public int emitTime;
            public float bulletForce;
        }
    }
}
