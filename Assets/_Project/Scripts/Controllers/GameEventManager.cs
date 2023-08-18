using System.Collections.Generic;
using UnityEngine;
using Utils;
using static UnityEngine.EventSystems.EventTrigger;

namespace dragoni7
{
    public class GameEventManager : Singleton<GameEventManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }
        public void Start()
        {
            EventSystem eventSystem = EventSystem.Instance;
            eventSystem.StartListening(Events.OnEntityDamaged, OnEntityDamaged);
            eventSystem.StartListening(Events.OnEntityMove, OnEntityMove);
            eventSystem.StartListening(Events.OnEntityAttack, OnEntityAttack);
            eventSystem.StartListening(Events.OnEnemySpawned, OnEnemySpawned);
            eventSystem.StartListening(Events.OnPlayerSpawned, OnPlayerSpawned);
            eventSystem.StartListening(Events.OnEntityDie, OnEntityDie);
        }
        private void OnEntityDamaged(Dictionary<string, object> eventArgs)
        {
            GameObject source = (GameObject)eventArgs["source"];
            Entity target = (Entity)eventArgs["target"];
            IDamage damage = (IDamage)eventArgs["damage"];
            DamageModifiers damageModifier = (DamageModifiers)eventArgs["damageModifier"];

            Vector3 targetPosition = target.transform.position;
            float damageTaken = damage.PerformDamage(damageModifier, target);

            UIController uiController = UIController.Instance;
            if (target is AbstractPlayer)
            {
                uiController.UpdatePlayerHealthBar(target.Attributes.health);
            }

            uiController.SendFloatingMessage(targetPosition + Random.insideUnitSphere, damageTaken.ToString(), damage.GetColor());

        }
        private void OnEntityMove(Dictionary<string, object> eventArgs)
        {
            Entity entity = (Entity)eventArgs["entity"];
            Vector3 moveThisFrame = (Vector2)eventArgs["moveThisFrame"];

            EntityController.Instance.MoveEntity(entity, moveThisFrame);
        }
        private void OnEntityAttack(Dictionary<string, object> eventArgs)
        {
            Entity entity = (Entity)eventArgs["Entity"];

            if (entity is AbstractPlayer)
            {
                UIController.Instance.ShakePlayerCamera(1f, 0.08f);
                PlayerController.Instance.PlayerAttack();
            }
            else
            {
                entity.PerformAttack();
            }
        }
        private void OnEnemySpawned(Dictionary<string, object> eventArgs)
        {
            AbstractEnemy spawnedEnemy = (AbstractEnemy)eventArgs["Enemy"];

            UIController.Instance.AddEntityHealthBar(spawnedEnemy);
        }
        private void OnPlayerSpawned(Dictionary<string, object> eventArgs)
        {
            AbstractPlayer spawnedPlayer = (AbstractPlayer)eventArgs["Player"];
            float health = spawnedPlayer.Attributes.health;

            UIController.Instance.UpdatePlayerHealthBarMaxHP(health);
            UIController.Instance.UpdatePlayerHealthBar(health);
        }
        private void OnEntityDie(Dictionary<string, object> eventArgs)
        {
            Entity killedEntity = (Entity)eventArgs["Entity"];

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
