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
                AbstractBullet bullet = Instantiate(Bullet.bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(bulletSpawnPoint.up * Stats.bulletForce, ForceMode2D.Impulse);
                attackCounter = 0;
            }

            attackCounter++;
        }

        public override void FixedUpdate()
        {
            Vector2 lookDir = mousePosition - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 45;
            rb.rotation = angle;
        }
    }
}
