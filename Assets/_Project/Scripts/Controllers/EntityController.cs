using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public class EntityController : Singleton<EntityController>
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
            EnemyData scriptableEnemy = ResourceSystem.Instance.GetEnemy(name);
            AbstractEnemy spawnedEnemy = Instantiate(scriptableEnemy.enemyPrefab, position, Quaternion.identity, transform);
            spawnedEnemy.SetAttributes(scriptableEnemy.BaseAttributes);

            // create enemy's emitter
            var scriptableEmitter = ResourceSystem.Instance.GetEmitter(scriptableEnemy.scriptableEmitter.name);
            BaseEmitter spawnedEmitter = Instantiate(scriptableEmitter.emitterPrefab, position, Quaternion.identity, spawnedEnemy.transform);
            spawnedEmitter.SetStats(scriptableEmitter.BaseStats);
            spawnedEmitter.pattern = scriptableEmitter.patternPrefab;
            spawnedEmitter.Bullet = scriptableEmitter.scriptableBullet;

            spawnedEnemy.Emitter = spawnedEmitter;

            // add enemy hp bar to canvas
            EventSystem.Instance.TriggerEvent(Events.OnEnemySpawned, new Dictionary<string, object> { { "Enemy", spawnedEnemy} });

            // create enemy ai
            AbstractBrain spawnedAI = Instantiate(scriptableEnemy.enemyAiPrefab, position, Quaternion.identity, spawnedEnemy.transform);
            spawnedEnemy.Brain = spawnedAI;
            CurrentEnemies.Add(spawnedEnemy);
        }
        public void MoveEntity(Entity entity, Vector2 moveThisFrame)
        {
            entity.rb.velocity = moveThisFrame * entity.Attributes.speed;
            entity.HealthBar.transform.position = entity.transform.position + (Vector3.up * 0.5f);
        }
    }
}
