using System;
using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractScriptableEntity : ScriptableObject
    {
        public EntityType type;

        [SerializeField] private Stats stats;
        public Stats BaseStats => stats;

        public string description;

        [Serializable]
        public struct Stats
        {
            public int health;
            public int damage;
            public float speed;
            public float shootingSpeed;
        }

        [Serializable]
        public enum EntityType
        {
            Player = 0,
            Friendly = 1,
            Neutral = 2,
            Enemy = 3
        }
    }
}
