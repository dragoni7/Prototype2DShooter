﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class ScriptableEntity : AbstractData
    {
        [SerializeField, SerializeReference] private List<AbstractAbility> abilities;
        public List<AbstractAbility> Abilities => abilities;

        [SerializeField] private EntityStats stats;
        public EntityStats BaseStats => stats;

        [Serializable]
        public struct EntityStats
        {
            public int health;
            public int damage;
            public float speed;
            public float shootingSpeed;
        }
    }
}
