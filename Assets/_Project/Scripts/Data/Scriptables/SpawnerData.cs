using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "Level/SpawnerData")]
    public class SpawnerData : ScriptableObject
    {
        [SerializeField]
        private int _spawnLimit;
        [SerializeField]
        private int _spawnTime;
        [SerializeField]
        private int _spawnAmountMin;
        [SerializeField]
        private int _spawnAmountMax;
        [SerializeField]
        private int _spread;
        [SerializeField]
        private string _enemy;

        public int SpawnLimit => _spawnLimit;
        public int SpawnTime => _spawnTime;
        public int SpawnAmountMin => _spawnAmountMin;
        public int SpawnAmountMax => _spawnAmountMax;
        public int Spread => _spread;
        public string Enemy => _enemy;
    }
}
