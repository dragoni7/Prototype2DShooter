﻿using System.Collections.Generic;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class WanderTask : Node
    {
        protected override void OnReset() { }

        private DirectionSolver _directionSolver;
        public WanderTask(List<AbstractSteeringBehaviour> behaviours)
        {
            _directionSolver = new DirectionSolver(behaviours);
        }

        protected override NodeStatus OnRun(AIData aiData)
        {
            if (aiData.currentTarget != null)
            {
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
