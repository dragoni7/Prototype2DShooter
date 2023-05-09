using System;
using System.Collections;
using UnityEngine;

namespace dragoni7
{
    [Serializable]
    public class DashAbility : AbstractAbility
    {
        private bool canDash = true;
        [SerializeField] private float dashingPower = 24f;
        [SerializeField] private float dashingTime = 0.2f;
        [SerializeField] private float dashingCooldown = 1f;
        public override IEnumerator Execute(Entity subject)
        {
            if (canDash)
            {
                canDash = false;
                subject.canMove = false;
                subject.canAttack = false;
                float originalGravity = subject.rb.gravityScale;
                subject.rb.gravityScale = 0f;
                subject.rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);

                yield return new WaitForSeconds(dashingTime);
                subject.rb.gravityScale = originalGravity;
                subject.canMove = true;
                subject.canAttack = true;
                subject.rb.velocity = Vector2.zero;
                yield return new WaitForSeconds(dashingCooldown);
                canDash = true;
            }
        }
    }
}
