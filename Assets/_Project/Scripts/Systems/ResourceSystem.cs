using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace dragoni7
{
    public class ResourceSystem : Singleton<ResourceSystem>
    {
        public List<PlayerData> ScriptablePlayers { get; private set; }
        private Dictionary<string, PlayerData> _playersDict;

        public List<EnemyData> ScriptableEnemies { get; private set; }
        private Dictionary<string, EnemyData> _enemiesDict;

        public List<WeaponData> ScriptableWeapons { get; private set; }
        private Dictionary<string, WeaponData> _weaponsDict;

        public List<BulletData> ScriptableBullets { get; private set; }
        private Dictionary<string, BulletData> _bulletsDict;

        public List<EmitterData> ScriptableEmitters { get; private set; }
        private Dictionary<string, EmitterData> _emittersDict;

        public List<LevelData> ScriptableLevelData { get; private set; }
        private Dictionary<string, LevelData> _levelDict;
        public List<LootTableData> ScriptableLootTableData { get; private set; }
        private Dictionary<string, LootTableData> _lootTableDict;

        protected override void Awake()
        {
            base.Awake();
            AssembleResources();
        }

        private void AssembleResources()
        {
            // Players
            ScriptablePlayers = Resources.LoadAll<PlayerData>("Players").ToList();
            ScriptablePlayers.ForEach(s => { s.BaseAttributes.Initialize(); });
            _playersDict = ScriptablePlayers.ToDictionary(r => r.name, r => r);

            // Enemies
            ScriptableEnemies = Resources.LoadAll<EnemyData>("Enemies").ToList();
            ScriptableEnemies.ForEach(s => { s.BaseAttributes.Initialize(); });
            _enemiesDict = ScriptableEnemies.ToDictionary(r => r.name, r => r);

            // Weapons
            ScriptableWeapons = Resources.LoadAll<WeaponData>("Weapons").ToList();
            _weaponsDict = ScriptableWeapons.ToDictionary(r => r.name, r => r);

            // Bullets
            ScriptableBullets = Resources.LoadAll<BulletData>("Bullets").ToList();
            _bulletsDict = ScriptableBullets.ToDictionary(r => r.name, r => r);

            // Emitters
            ScriptableEmitters = Resources.LoadAll<EmitterData>("Emitters").ToList();
            _emittersDict = ScriptableEmitters.ToDictionary(r => r.name, r => r);

            // Level
            ScriptableLevelData = Resources.LoadAll<LevelData>("Level").ToList();
            _levelDict = ScriptableLevelData.ToDictionary(r => r.name, r => r);

            // Loot Tables
            ScriptableLootTableData = Resources.LoadAll<LootTableData>("LootTables").ToList();
            _lootTableDict = ScriptableLootTableData.ToDictionary(r => r.name, r => r);
        }
        public PlayerData GetPlayer(string name) => _playersDict[name];
        public EnemyData GetEnemy(string name) => _enemiesDict[name];
        public WeaponData GetWeapon(string name) => _weaponsDict[name];
        public BulletData GetBullet(string name) => _bulletsDict[name];
        public EmitterData GetEmitter(string name) => _emittersDict[name];
        public LevelData GetLevelData(string name) => _levelDict[name];
        public LootTableData GetLootTableData(string name) => _lootTableDict[name];
    }
}
