using UnityEngine;
using static dragoni7.ScriptableBullet;

namespace dragoni7
{
    public class AbstractBullet : Entity
    {
        public Vector2 Velocity { get; set; }
        public float BulletForce { get; set; }
        public BulletStats Stats { get; protected set; }

        private int timer;

        public virtual void Start()
        {
            canMove = true;
            canAttack = true;
            timer = 0;
        }

        public void SetStats(BulletStats stats)
        {
            Stats = stats;
        }
        public virtual void Update()
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 newPosition = currentPosition + Velocity * Time.deltaTime * BulletForce;

            RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

            foreach (RaycastHit2D hit in hits)
            {
                gameObject.SetActive(false);
            }

            transform.position = newPosition;
        }

        public virtual void FixedUpdate()
        {
            if (!canMove)
            {
                return;
            }

            if (timer >= Stats.duration)
            {
                timer = 0;
                gameObject.SetActive(false);
            }

            timer++;
        }
    }
}
