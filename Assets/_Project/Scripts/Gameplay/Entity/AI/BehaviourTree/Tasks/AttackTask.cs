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
            GameEventManager.Instance.EventBus.Raise(new EntityAttackEvent { entity = aiData.entity });
            return NodeStatus.Running;
        }
    }
}
