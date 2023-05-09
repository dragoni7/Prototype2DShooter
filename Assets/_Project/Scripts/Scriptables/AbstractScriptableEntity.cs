using System;
using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractScriptableEntity : ScriptableObject
    {
        public EntityType type;

        [SerializeField, SerializeReference] private List<AbstractAbility> abilities;
        public List<AbstractAbility> Abilities => abilities;

        public string entityName;

        public string description;

        [Serializable]
        public enum EntityType
        {
            Player = 0,
            Friendly = 1,
            Neutral = 2,
            Enemy = 3,
            Bullet = 4
        }
    }
}
