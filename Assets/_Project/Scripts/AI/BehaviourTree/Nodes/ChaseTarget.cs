using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class ChaseTarget : Node
    {
        protected override void OnReset() { }
        protected override NodeStatus OnRun(AIData aiData)
        {
            aiData.following = true;

            if (aiData.currentTarget == null)
            {
                // stop logic
                aiData.movementInput = Vector2.zero;
                aiData.following = false;
                return NodeStatus.Failure;
            }
            else
            {
                aiData.movementInput = aiData.movementDirectionSolver.GetDirectionToMove(aiData.behaviours, aiData);
            }

            aiData.rb.velocity = aiData.movementInput * 2;
            return NodeStatus.Running;
        }
    }
}
