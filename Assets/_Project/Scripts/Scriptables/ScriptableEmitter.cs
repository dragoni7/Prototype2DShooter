using System;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Emitter")]
    public class ScriptableEmitter : AbstractScriptable
    {
        public BaseEmitter emitterPrefab;
        public ScriptableBullet scriptableBullet;
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
