using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace dragoni7
{
    public class TestWeapon : AbstractWeapon
    {
        public override void PerformAttack()
        {
            if (attackCounter == Stats.attackCooldown)
            {
                BulletController.Instance.SpawnBullet(Bullet, bulletSpawnPoint.position, Quaternion.Euler(0, 0, angle + 90), new Vector2(lookDirection.x, lookDirection.y).normalized, Stats.bulletForce);
                attackCounter = 0;
            }

            attackCounter++;
        }
    }
}
