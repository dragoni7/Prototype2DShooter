using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class AttackTask : Node
    {
        protected override void OnReset() { }

        protected override NodeStatus OnRun(AIData aiData)
        {
            aiData.MovementInput = Vector2.zero;
            EventSystem.Instance.TriggerEvent(Events.OnEntityAttack, new Dictionary<string, object> { { "Entity", aiData.entity } });
            return NodeStatus.Running;
        }
    }
}
