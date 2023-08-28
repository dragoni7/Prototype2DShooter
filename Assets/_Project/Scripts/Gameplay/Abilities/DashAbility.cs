using System;
using System.Collections;
using UnityEngine;

namespace dragoni7
{
    /// <summary>
    /// Performs a dash on an entity. Entity must have a trail renderer component
    /// </summary>
    [Serializable]
    public class DashAbility : AbstractAbility
    {
        private bool _canDash = true;
        private TrailRenderer _trailRenderer;
        [SerializeField] 
        private float _dashingPower = 14f;
        [SerializeField] 
        private float _dashingTime = 0.5f;
        [SerializeField] 
        private float _dashingCooldown = 1f;
        public override IEnumerator Execute(Entity entity)
        {
            if (_canDash)
            {
                _trailRenderer = entity.GetComponent<TrailRenderer>();

                _canDash = false;
                entity.CanMove = false;
                entity.CanAttack = false;

                entity.rb.velocity = entity.rb.velocity.normalized * _dashingPower;

                _trailRenderer.time = _dashingTime;
                _trailRenderer.emitting = true;

                yield return new WaitForSeconds(_dashingTime);

                entity.CanMove = true;
                entity.CanAttack = true;
                entity.rb.velocity = Vector2.zero;
                _trailRenderer.emitting = false;

                yield return new WaitForSeconds(_dashingCooldown);
                _canDash = true;
            }
        }
    }
}
