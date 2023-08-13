using System.Collections.Generic;
using UnityEngine;
using Utils;

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
            EventSystem.Instance.StartListening(Events.OnEntityDamaged, OnEntityDamaged);
            EventSystem.Instance.StartListening(Events.OnEntityMove, OnEntityMove);
            EventSystem.Instance.StartListening(Events.OnEntityAttack, OnEntityAttack);
            EventSystem.Instance.StartListening(Events.OnPlayerAttack, OnPlayerAttack);
        }
        private void OnEntityDamaged(Dictionary<string, object> eventArgs)
        {
            GameObject source = (GameObject)eventArgs["source"];
            Entity target = (Entity)eventArgs["target"];
            IDamage damage = (IDamage)eventArgs["damage"];
            DamageModifiers damageModifier = (DamageModifiers)eventArgs["damageModifier"];

            damage.PerformDamage(damageModifier, target);
        }
        private void OnEntityMove(Dictionary<string, object> eventArgs)
        {
            Entity entity = (Entity)eventArgs["entity"];
            Vector3 moveThisFrame = (Vector2)eventArgs["moveThisFrame"];

            EntityController.Instance.MoveEntity(entity, moveThisFrame);
        }
        private void OnEntityAttack(Dictionary<string, object> eventArgs)
        {
            Entity entity = (Entity)eventArgs["entity"];
            entity.PerformAttack();
        }
        private void OnPlayerAttack(Dictionary<string, object> eventArgs)
        {
            CameraShake.Instance.ShakeCamera(1f, 0.08f);
            PlayerController.Instance.PlayerAttack();
        }
    }
}
