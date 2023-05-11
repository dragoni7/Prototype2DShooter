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

        public List<ScriptableEmitter> ScriptableEmitters { get; private set; }
        private Dictionary<string, ScriptableEmitter> emittersDict;

        protected override void Awake()
        {
            base.Awake();
            AssembleResources();
        }

        private void AssembleResources()
        {
            // Players
            ScriptablePlayers = Resources.LoadAll<ScriptablePlayer>("Players").ToList();
            playersDict = ScriptablePlayers.ToDictionary(r => r.objectName, r => r);

            // Enemies

            // Weapons
            ScriptableWeapons = Resources.LoadAll<ScriptableWeapon>("Weapons").ToList();
            weaponsDict = ScriptableWeapons.ToDictionary(r => r.objectName, r => r);

            // Bullets
            ScriptableBullets = Resources.LoadAll<ScriptableBullet>("Bullets").ToList();
            bulletsDict = ScriptableBullets.ToDictionary(r => r.objectName, r => r);

            // Emitters
            ScriptableEmitters = Resources.LoadAll<ScriptableEmitter>("Emitters").ToList();
            emittersDict = ScriptableEmitters.ToDictionary(r => r.objectName, r => r);
        }

        public ScriptablePlayer GetPlayer(string name) => playersDict[name];

        public ScriptableWeapon GetWeapon(string name) => weaponsDict[name];

        public ScriptableBullet GetBullet(string name) => bulletsDict[name];

        public ScriptableEmitter GetEmitter(string name) => emittersDict[name];
    }
}
