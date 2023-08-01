﻿using System;
using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractData : ScriptableObject
    {
        public Type type;

        public string displayName;

        public string description;

        [Serializable]
        public enum Type
        {
            Player = 0,
            Friendly = 1,
            Neutral = 2,
            Enemy = 3,
            Bullet = 4,
            Emitter = 5
        }
    }
}
