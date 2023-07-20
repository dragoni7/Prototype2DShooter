using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class DetectObstaclesTask : Node
    {
        private float _detectionRange;
        private LayerMask _obstacleLayer = LayerMask.GetMask("Obstacle", "Enemy");

        private Collider2D[] _colliders;
        public DetectObstaclesTask(float detectionRange)
        {
            _detectionRange = detectionRange;
        }
        protected override void OnReset() { }
        protected override NodeStatus OnRun(AIData aiData)
        {
            _colliders = Physics2D.OverlapCircleAll(aiData.transform.position, _detectionRange, _obstacleLayer);
            aiData.obstacles = _colliders;
            return NodeStatus.Success;
        }
    }
}
