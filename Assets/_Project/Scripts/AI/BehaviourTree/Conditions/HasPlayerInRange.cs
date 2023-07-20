using System.Collections.Generic;
using UnityEngine;
using WUG.BehaviorTreeVisualizer;

namespace dragoni7
{
    public class HasPlayerInRange : Condition
    {
        private float _detectionRange;

        private LayerMask _playerLayer = LayerMask.GetMask("Player");
        private LayerMask _obstacleLayer = LayerMask.GetMask("Player", "Obstacle");

        private List<Transform> _colliders;
        public HasPlayerInRange(float detectionRange) : base("Has player in range?")
        {
            _detectionRange = detectionRange;
        }
        protected override void OnReset() { }
        protected override NodeStatus OnRun(AIData aiData)
        {
            // Find if player is near
            Collider2D playerCollider = Physics2D.OverlapCircle(aiData.transform.position, _detectionRange, _playerLayer);

            if (playerCollider != null)
            {
                // Check if can see player
                Vector2 direction = (playerCollider.transform.position - aiData.transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(aiData.transform.position, direction, _detectionRange, _obstacleLayer);
                aiData.attackDirection = direction;

                // Make sure that the collider we see is on Player layer
                if (hit.collider != null && (_playerLayer & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    Debug.DrawRay(aiData.transform.position, direction * _detectionRange, Color.magenta);
                    _colliders = new List<Transform>() { playerCollider.transform };
                }
                else
                {
                    _colliders = null;
                    aiData.attackDirection = Vector3.zero;
                }
            }
            else
            {
                // Enemy doesnt see player
                _colliders = null;
                aiData.attackDirection = Vector3.zero;
            }

            aiData.targets = _colliders;

            if (_colliders == null)
            {
                return NodeStatus.Failure;
            }

            return NodeStatus.Success;
        }
    }
}
