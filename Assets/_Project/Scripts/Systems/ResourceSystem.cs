using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace dragoni7
{
    public class ResourceSystem : Singletone<ResourceSystem>
    {
        public List<ScriptablePlayer> ScriptablePlayers { get; private set; }
        private Dictionary<string, ScriptablePlayer> playersDict;

        public List<ScriptableWeapon> ScriptableWeapons { get; private set; }
        private Dictionary<string, ScriptableWeapon> weaponsDict;

        public List<ScriptableBullet> ScriptableBullets { get; private set; }
        private Dictionary<string, ScriptableBullet> bulletsDict;

        protected override void Awake()
        {
            base.Awake();
            AssembleResources();
        }

        private void AssembleResources()
        {
            // Players
            ScriptablePlayers = Resources.LoadAll<ScriptablePlayer>("Players").ToList();
            playersDict = ScriptablePlayers.ToDictionary(r => r.entityName, r => r);

            // Enemies

            // Weapons
            ScriptableWeapons = Resources.LoadAll<ScriptableWeapon>("Weapons").ToList();
            weaponsDict = ScriptableWeapons.ToDictionary(r => r.entityName, r => r);

            // Bullets
            ScriptableBullets = Resources.LoadAll<ScriptableBullet>("Bullets").ToList();
            bulletsDict = ScriptableBullets.ToDictionary(r => r.entityName, r => r);
        }

        public ScriptablePlayer GetPlayer(string name) => playersDict[name];

        public ScriptableWeapon GetWeapon(string name) => weaponsDict[name];

        public ScriptableBullet GetBullet(string name) => bulletsDict[name];
    }
}
