using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class AttackTask : Node
    {
        protected override void OnReset() { }

        protected override NodeStatus OnRun(AIData aiData)
        {
            aiData.OnMove?.Invoke(Vector3.zero);
            aiData.OnAttack?.Invoke(aiData.attackDirection);
            return NodeStatus.Running;
        }
    }
}
