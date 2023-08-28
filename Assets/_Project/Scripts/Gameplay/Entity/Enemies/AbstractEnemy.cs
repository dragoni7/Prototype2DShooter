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
            Brain.AIData.entity = this;
        }
        public override void TakeDamage(float damage)
        {
            if (gameObject.activeSelf)
            {
                CurrentHealth -= damage;
            }
        }

        public override void Heal(float amount)
        {
            CurrentHealth += amount;
        }

        public override void Die()
        {
            base.Die();
            Destroy(gameObject);
            Destroy(HealthBar.gameObject);
        }
    }
}