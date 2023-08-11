using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public class GameEventManager : Singletone<GameEventManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void Start()
        {
            EventSystem.Instance.StartListening(EventSystem.Events.OnEntityDamaged, OnEntityDamaged);
        }
        private void OnEntityDamaged(Dictionary<string, object> eventArgs)
        {
            GameObject source = (GameObject)eventArgs["source"];
            Entity target = (Entity)eventArgs["target"];
            IDamage damage = (IDamage)eventArgs["damage"];
            DamageModifiers damageModifier = (DamageModifiers)eventArgs["damageModifier"];

            damage.PerformDamage(damageModifier, target);
        }
    }
}
