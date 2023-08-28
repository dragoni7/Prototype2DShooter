using System.Collections.Generic;
using UnityEngine;
using Util;

namespace dragoni7
{

    /// <summary>
    /// Mediator class for handling events triggered by objects
    /// </summary>
    public class GameEventManager : Singleton<GameEventManager>
    {
        private GenericEventBus<IEvent> _eventBus;
        public GenericEventBus<IEvent> EventBus => _eventBus;
        protected override void Awake()
        {
            base.Awake();
            _eventBus = new();
        }
        public void Start()
        {
            // subscribe to all relevant events
            // allow other objects to raise events when needed
            _eventBus.SubscribeTo<EntityDamagedEvent>(OnEntityDamaged);
            _eventBus.SubscribeTo<EntityHealthChangedEvent>(OnEntityHealthChanged);
            _eventBus.SubscribeTo<EntityMoveEvent>(OnEntityMove);
            _eventBus.SubscribeTo<PlayerAttackEvent>(OnPlayerAttack);
            _eventBus.SubscribeTo<EntityAttackEvent>(OnEntityAttack);
            _eventBus.SubscribeTo<EnemySpawnedEvent>(OnEnemySpawned);
            _eventBus.SubscribeTo<PlayerSpawnEvent>(OnPlayerSpawned);
            _eventBus.SubscribeTo<EntityDeathEvent>(OnEntityDie);
        }
        private void OnEntityDamaged(ref EntityDamagedEvent e)
        {
            GameObject source = e.source;
            Entity target = e.target;
            IDamage damage = e.damage;
            Attributes attributes = e.attackerAttributes;

            Vector3 targetPosition = target.transform.position;
            float damageTaken = damage.PerformDamage(attributes, target);

            UIController uiController = UIController.Instance;

            uiController.SendFloatingMessage(targetPosition + Random.insideUnitSphere, damageTaken.ToString(), damage.GetColor());
        }
        private void OnEntityHealthChanged(ref EntityHealthChangedEvent e)
        {
            Entity entity = e.entity;

            UIController uiController = UIController.Instance;

            float health = entity.CurrentHealth;

            if (entity is AbstractPlayer)
            {
                uiController.UpdatePlayerHealthBar(health);
            }
            else
            {
                entity.HealthBar.SetHealth(health);
            }
        }
        private void OnEntityMove(ref EntityMoveEvent e)
        {
            Entity entity = e.entity;
            Vector3 moveThisFrame = e.moveThisFrame;

            EntityController.Instance.MoveEntity(entity, moveThisFrame);
        }
        private void OnEntityAttack(ref EntityAttackEvent e)
        {
            Entity entity = e.entity;
            entity.PerformAttack();
        }
        private void OnPlayerAttack(ref PlayerAttackEvent e)
        {
            UIController.Instance.ShakePlayerCamera(1f, 0.08f);
            PlayerController.Instance.PlayerAttack();
        }
        private void OnEnemySpawned(ref EnemySpawnedEvent e)
        {
            AbstractEnemy spawnedEnemy = e.enemy;

            UIController.Instance.AddEntityHealthBar(spawnedEnemy);
        }
        private void OnPlayerSpawned(ref PlayerSpawnEvent e)
        {
            AbstractPlayer spawnedPlayer = e.player;
            float health = spawnedPlayer.MaxHealth;

            UIController.Instance.UpdatePlayerHealthBarMaxHP(health);
            UIController.Instance.UpdatePlayerHealthBar(health);
        }
        private void OnEntityDie(ref EntityDeathEvent e)
        {
            Entity killedEntity = e.entity;

            if (killedEntity is AbstractPlayer)
            {
                // reset logic
            }
            else if (killedEntity is AbstractEnemy)
            {
                // drop stuff, increase score ect

                // get loot table for entity and instantiate drop item
                // TODO: move to a controller class
                ItemData itemData = ResourceSystem.Instance.GetLootTableData(killedEntity.name.Split("(Clone)")[0]).GetDrop();
                Item item = Instantiate(itemData.prefab, killedEntity.transform.position + Random.insideUnitSphere, Quaternion.identity);
                item.GetComponent<SpriteRenderer>().sprite = itemData.sprite;
            }
        }
    }
}
