using UnityEngine;

namespace dragoni7
{
    public class Enemy1 : AbstractEnemy
    {
        public override void PerformAttack()
        {
            Vector3 direction = Brain.AIData.attackDirection;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Emitter.transform.rotation = Quaternion.Euler(0, 0, angle);
            Emitter.TryEmitBullets(CurrentAttributes);
        }
    }
}
