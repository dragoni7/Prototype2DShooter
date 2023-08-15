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
            Emitter.TryEmitBullets(Attributes.damageModifiers);
        }
        public override void TakeDamage(float damage)
        {
            if (gameObject.activeSelf)
            {
                _attributes.health -= damage;
                HealthBar.SetHealth(Attributes.health);

                if (Attributes.health <= 0)
                {
                    //gameObject.SetActive(false);
                    //HealthBar.gameObject.SetActive(false);
                    Destroy(gameObject);
                    Destroy(HealthBar.gameObject);
                }
            }
        }
    }
}
