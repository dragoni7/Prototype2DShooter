using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class HasTarget : Condition
    {
        public HasTarget() : base("Has Target?") { }

        protected override void OnReset() { }

        protected override NodeStatus OnRun(AIData aiData)
        {
            if (aiData == null)
            {
                StatusReason = "aiData is null";
                aiData.OnMove?.Invoke(Vector3.zero);
                return NodeStatus.Failure;
            }

            if (aiData.currentTarget == null)
            {
                if (aiData.GetTargetsCount() > 0)
                {
                    aiData.currentTarget = aiData.targets[0];
                    StatusReason = "Current target set to next target";
                    return NodeStatus.Success;
                }

                aiData.OnMove?.Invoke(Vector3.zero);
                StatusReason = "aiData has no targets";
                return NodeStatus.Failure;
            }

            return NodeStatus.Success;
        }
    }
}
