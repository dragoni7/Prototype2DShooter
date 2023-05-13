using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace dragoni7
{
    public class ResourceSystem : Singletone<ResourceSystem>
    {
        public List<ScriptablePlayer> ScriptablePlayers { get; private set; }
        private Dictionary<string, ScriptablePlayer> _playersDict;

        public List<ScriptableEnemy> ScriptableEnemies { get; private set; }
        private Dictionary<string, ScriptableEnemy> _enemiesDict;

        public List<ScriptableWeapon> ScriptableWeapons { get; private set; }
        private Dictionary<string, ScriptableWeapon> _weaponsDict;

        public List<ScriptableBullet> ScriptableBullets { get; private set; }
        private Dictionary<string, ScriptableBullet> _bulletsDict;

        public List<ScriptableEmitter> ScriptableEmitters { get; private set; }
        private Dictionary<string, ScriptableEmitter> _emittersDict;

        protected override void Awake()
        {
            base.Awake();
            AssembleResources();
        }

        private void AssembleResources()
        {
            // Players
            ScriptablePlayers = Resources.LoadAll<ScriptablePlayer>("Players").ToList();
            _playersDict = ScriptablePlayers.ToDictionary(r => r.name, r => r);

            // Enemies
            ScriptableEnemies = Resources.LoadAll<ScriptableEnemy>("Enemies").ToList();
            _enemiesDict = ScriptableEnemies.ToDictionary(r => r.name, r => r);

            // Weapons
            ScriptableWeapons = Resources.LoadAll<ScriptableWeapon>("Weapons").ToList();
            _weaponsDict = ScriptableWeapons.ToDictionary(r => r.name, r => r);

            // Bullets
            ScriptableBullets = Resources.LoadAll<ScriptableBullet>("Bullets").ToList();
            _bulletsDict = ScriptableBullets.ToDictionary(r => r.name, r => r);

            // Emitters
            ScriptableEmitters = Resources.LoadAll<ScriptableEmitter>("Emitters").ToList();
            _emittersDict = ScriptableEmitters.ToDictionary(r => r.name, r => r);
        }

        public ScriptablePlayer GetPlayer(string name) => _playersDict[name];
        public ScriptableEnemy GetEnemy(string name) => _enemiesDict[name];

        public ScriptableWeapon GetWeapon(string name) => _weaponsDict[name];

        public ScriptableBullet GetBullet(string name) => _bulletsDict[name];

        public ScriptableEmitter GetEmitter(string name) => _emittersDict[name];
    }
}
