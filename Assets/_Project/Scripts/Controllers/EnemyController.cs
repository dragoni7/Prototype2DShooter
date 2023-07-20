using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public class EnemyController : Singletone<EnemyController>
    {
        public List<AbstractEnemy> CurrentEnemies { get; set; }

        protected override void Awake()
        {
            CurrentEnemies = new();
            base.Awake();
        }

        public void SpawnEnemy(string name, Vector2 position)
        {
            // create enemy
            var scriptableEnemy = ResourceSystem.Instance.GetEnemy(name);
            AbstractEnemy spawnedEnemy = Instantiate(scriptableEnemy.enemyPrefab, position, Quaternion.identity, transform);
            spawnedEnemy.SetStats(scriptableEnemy.BaseStats);

            // create enemie's emitter
            var scriptableEmitter = ResourceSystem.Instance.GetEmitter(scriptableEnemy.scriptableEmitter.name);
            BaseEmitter spawnedEmitter = Instantiate(scriptableEmitter.emitterPrefab, position, Quaternion.identity, spawnedEnemy.transform);
            spawnedEmitter.SetStats(scriptableEmitter.BaseStats);
            spawnedEmitter.pattern = scriptableEmitter.patternPrefab;
            spawnedEmitter.Bullet = scriptableEmitter.scriptableBullet;

            spawnedEnemy.Emitter = spawnedEmitter;

            // create enemy ai
            AbstractBrain spawnedAI = Instantiate(scriptableEnemy.enemyAiPrefab, position, Quaternion.identity, spawnedEnemy.transform);
            spawnedEnemy.Brain = spawnedAI;

            CurrentEnemies.Add(spawnedEnemy);
        }

        public void FixedUpdate()
        {
            foreach (AbstractEnemy enemy in CurrentEnemies)
            {
                //enemy.PerformAttack();
            }
        }
    }
}
