using System;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Emitter")]
    public class EmitterData : ScriptableObject
    {
        public BaseEmitter emitterPrefab;
        public BulletData scriptableBullet;
        public BasePattern patternPrefab;

        [SerializeField] private EmitterAttributes attributes;
        public EmitterAttributes BaseAttributes => attributes;

        [Serializable]
        public struct EmitterAttributes
        {
            public int emitTime;
            public float bulletForce;
        }
    }
}
