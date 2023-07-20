using WUG.BehaviorTreeVisualizer;
using UnityEngine;

namespace dragoni7
{
    public class IsTargetWithinRange : Condition
    {
        private float _distance;
        public IsTargetWithinRange(float distance) : base($"Is Target Within {distance}?")
        {
            _distance = distance;
        }

        protected override void OnReset() { }

        protected override NodeStatus OnRun(AIData aiData)
        {
            float distance = (aiData.currentTarget.position - aiData.transform.position).magnitude;

            if (distance <= _distance)
            {
                return NodeStatus.Success;
            }

            return NodeStatus.Failure;
        }
    }
}
