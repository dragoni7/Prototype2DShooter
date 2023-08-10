using System;
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
            EventSystem.Instance.StartListening(EventSystem.Events.OnTakeDamage, OnDestructableDamaged);
        }
        private void OnDestructableDamaged(Dictionary<string, object> eventArgs)
        {
            GameObject source = (GameObject)eventArgs["source"];
            IDestructable target = (IDestructable)eventArgs["target"];
            int damage = (int)eventArgs["damage"];
            target.TakeDamage(damage);
        }
    }
}
