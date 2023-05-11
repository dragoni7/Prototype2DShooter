using System;
using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName ="New Scriptable Player")]
    public class ScriptablePlayer : AbstractScriptable
    {
        public AbstractPlayer playerPrefab;

        public ScriptableWeapon scriptableWeapon;

        [SerializeField, SerializeReference] private List<AbstractAbility> abilities;
        public List<AbstractAbility> Abilities => abilities;

        public Vector2 equipPos;

        [SerializeField] private PlayerStats stats;
        public PlayerStats BaseStats => stats;

        [Serializable]
        public struct PlayerStats
        {
            public int health;
            public int damage;
            public float speed;
            public float shootingSpeed;
        }
    }
}
