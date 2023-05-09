using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static dragoni7.ScriptablePlayer;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Weapon")]
    public class ScriptableWeapon : AbstractScriptableEntity
    {
        public AbstractWeapon weaponPrefab;

        public ScriptableBullet scriptableBullet;

        [SerializeField] private WeaponStats stats;
        public WeaponStats BaseStats => stats;

        [Serializable]
        public struct WeaponStats
        {
            public int damage;
            public int attackCooldown;
            public float bulletForce;
        }

    }
}
