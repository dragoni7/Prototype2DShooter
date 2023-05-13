using System;
using System.Collections;
using UnityEngine;

namespace dragoni7
{
    [Serializable]
    public class DashAbility : AbstractAbility
    {
        private bool _canDash = true;
        private TrailRenderer _trailRenderer;
        [SerializeField] private float _dashingPower = 14f;
        [SerializeField] private float _dashingTime = 0.5f;
        [SerializeField] private float _dashingCooldown = 1f;
        public override IEnumerator Execute(BaseEntity subject)
        {
            if (_canDash)
            {
                _trailRenderer = subject.GetComponent<TrailRenderer>();

                _canDash = false;
                subject.canMove = false;
                subject.canAttack = false;

                subject.rb.velocity = subject.rb.velocity.normalized * _dashingPower;

                _trailRenderer.time = _dashingTime;
                _trailRenderer.emitting = true;

                yield return new WaitForSeconds(_dashingTime);

                subject.canMove = true;
                subject.canAttack = true;
                subject.rb.velocity = Vector2.zero;
                _trailRenderer.emitting = false;

                yield return new WaitForSeconds(_dashingCooldown);
                _canDash = true;
            }
        }
    }
}
