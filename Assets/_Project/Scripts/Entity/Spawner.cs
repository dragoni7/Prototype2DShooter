using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace dragoni7
{
    public class Spawner
    {
        private SpawnerData _spawnData;

        private int _timer = 0;
        private int _spawnCount = 0;
        private Vector2 _position;
        public Vector2 Position => _position;

        public Spawner(Vector2 position, SpawnerData spawnData)
        {
            _spawnData = spawnData;
            _timer = _spawnData.SpawnTime;
            _position = position;
        }

        public void TrySpawn()
        {
            if (_spawnCount < _spawnData.SpawnLimit || _spawnData.SpawnLimit == 0)
            {
                _timer++;

                if (_timer >= _spawnData.SpawnTime)
                {
                    _timer = 0;

                    int spawnAmount = Random.Range(_spawnData.SpawnAmountMin, _spawnData.SpawnAmountMax + 1);

                    for (int i = 0; i < spawnAmount; i++)
                    {
                        EnemyController.Instance.SpawnEnemy(_spawnData.Enemy, _position + (Random.insideUnitCircle * _spawnData.Spread));
                        _spawnCount++;
                    }
                }
            }
        }
    }
}
