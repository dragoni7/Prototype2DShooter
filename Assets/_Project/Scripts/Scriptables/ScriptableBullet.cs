using System;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Bullet")]
    public class ScriptableBullet : AbstractScriptableEntity
    {
        public AbstractBullet bulletPrefab;

        [SerializeField] private BulletStats stats;
        public BulletStats BaseStats => stats;
        [Serializable]
        public struct BulletStats
        {
            public int damage;
        }
    }
}
