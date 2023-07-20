using WUG.BehaviorTreeVisualizer;
using UnityEngine;

namespace dragoni7
{
    public class IsFollowingTarget : Condition
    {
        public IsFollowingTarget(string name) : base(name) { }

        protected override void OnReset() { }

        protected override NodeStatus OnRun(AIData aiData)
        {
            if (aiData == null)
            {
                StatusReason = "aiData is null";
                //aiData.rb.velocity = Vector2.zero;
                return NodeStatus.Failure;
            }

            if (aiData.following)
            {
                return NodeStatus.Success;
            }

            //aiData.rb.velocity = Vector2.zero;
            return NodeStatus.Failure;
        }
    }
}
