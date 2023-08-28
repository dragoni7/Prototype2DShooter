using System.Collections.Generic;
using UnityEngine;
using Util;

namespace dragoni7
{
    /// <summary>
    /// Controller class for non player entities
    /// </summary>
    public class EntityController : Singleton<EntityController>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Instantiates a new enemy entity
        /// </summary>
        /// <param name="name">name of the enemy type</param>
        /// <param name="position">position to spawn at</param>
        public void SpawnEnemy(string name, Vector2 position)
        {
            // create enemy
            EnemyData scriptableEnemy = ResourceSystem.Instance.GetEnemy(name);
            AbstractEnemy spawnedEnemy = Instantiate(scriptableEnemy.enemyPrefab, position, Quaternion.identity, transform);
            spawnedEnemy.SetAttributes(scriptableEnemy.BaseAttributes);

            // create enemy's emitter
            EmitterData scriptableEmitter = ResourceSystem.Instance.GetEmitter(scriptableEnemy.scriptableEmitter.name);
            BaseEmitter spawnedEmitter = Instantiate(scriptableEmitter.emitterPrefab, position, Quaternion.identity, spawnedEnemy.transform);
            spawnedEmitter.SetAttributes(scriptableEmitter.BaseAttributes);
            spawnedEmitter.Pattern = Instantiate(scriptableEmitter.patternPrefab, spawnedEmitter.transform.position, Quaternion.identity, spawnedEmitter.transform);
            spawnedEmitter.Bullet = scriptableEmitter.scriptableBullet;

            spawnedEnemy.Emitter = spawnedEmitter;

            // create enemy ai
            AbstractBrain spawnedAI = Instantiate(scriptableEnemy.enemyAiPrefab, position, Quaternion.identity, spawnedEnemy.transform);
            spawnedEnemy.Brain = spawnedAI;

            // add enemy hp bar to canvas
            GameEventManager.Instance.EventBus.Raise(new EnemySpawnedEvent { enemy = spawnedEnemy});
        }

        /// <summary>
        /// Moves an entity
        /// </summary>
        /// <param name="entity">entity to move</param>
        /// <param name="moveThisFrame">entity's movement</param>
        public void MoveEntity(Entity entity, Vector2 moveThisFrame)
        {
            entity.rb.velocity = moveThisFrame * entity.CurrentSpeed;
            entity.HealthBar.transform.position = entity.transform.position + (Vector3.up * 0.5f);
        }
    }
}
