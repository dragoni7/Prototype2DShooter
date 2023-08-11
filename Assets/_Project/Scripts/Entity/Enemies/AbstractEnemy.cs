using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractEnemy : Entity
    {
        public BaseEmitter Emitter { get; set; }
        public AbstractBrain Brain { get; set; }
        public void Start()
        {
            CanMove = true;
            CanAttack = true;
            Brain.AIData.OnAttack += PerformAttack;
            Brain.AIData.OnMove += Move;
        }
        public virtual void PerformAttack(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Emitter.transform.rotation = Quaternion.Euler(0, 0, angle);
            Emitter.TryEmitBullets(Attributes.damageModifiers);
        }
        public void Move(Vector3 moveThisFrame)
        {
            rb.velocity = moveThisFrame * Attributes.speed;
        }
        public override void TakeDamage(float damage)
        {
            if (gameObject.activeSelf)
            {
                _attributes.health -= damage;
                
                if (Attributes.health <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}