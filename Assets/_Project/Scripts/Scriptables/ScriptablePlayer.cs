using System;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName ="New Scriptable Player")]
    public class ScriptablePlayer : AbstractScriptableEntity
    {
        public AbstractPlayer playerPrefab;

        public ScriptableWeapon scriptableWeapon;

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
