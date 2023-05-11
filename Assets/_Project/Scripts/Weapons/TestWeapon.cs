using UnityEngine;

namespace dragoni7
{
    public class TestWeapon : AbstractWeapon
    {
        public override void PerformAttack()
        {
            Emitter.EmitBullets();
        }
    }
}
