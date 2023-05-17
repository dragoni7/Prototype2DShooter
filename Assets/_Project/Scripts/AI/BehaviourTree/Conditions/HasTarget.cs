using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class HasTarget : Condition
    {
        public HasTarget(string name) : base(name) { }

        protected override void OnReset() { }

        protected override NodeStatus OnRun(AIData aiData)
        {
            if (aiData == null)
            {
                StatusReason = "aiData is null";
                aiData.rb.velocity = Vector2.zero;
                return NodeStatus.Failure;
            }

            if (aiData.currentTarget == null)
            {
                if (aiData.GetTargetsCount() > 0)
                {
                    aiData.currentTarget = aiData.targets[0];
                    StatusReason = "Current target successfully set";
                    return NodeStatus.Success;
                }

                aiData.rb.velocity = Vector2.zero;
                StatusReason = "aiData has no targets";
                return NodeStatus.Failure;
            }

            return NodeStatus.Success;
        }
    }
}
