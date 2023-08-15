using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class PursueTargetTask : Node
    {
        private DirectionSolver _directionSolver;
        public PursueTargetTask(List<AbstractSteeringBehaviour> behaviours)
        {
            _directionSolver = new DirectionSolver(behaviours);
        }
        protected override void OnReset() { }
        protected override NodeStatus OnRun(AIData aiData)
        {
            aiData.following = true;

            if (aiData.currentTarget == null)
            {
                // stop logic
                aiData.MovementInput = Vector2.zero;
                aiData.following = false;
                return NodeStatus.Failure;
            }
            else
            {
                aiData.MovementInput = _directionSolver.GetDirectionToMove(aiData);
            }

            return NodeStatus.Success;
        }
    }
}
