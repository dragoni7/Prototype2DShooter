using System.Linq;
using UnityEngine;
using static dragoni7.ScriptableBullet;

namespace dragoni7
{
    public class AbstractBullet : MonoBehaviour
    {
        public Vector2 Velocity { get; set; }
        public float BulletForce { get; set; }
        public BulletStats Stats { get; protected set; }

        private int timer;

        public virtual void Start()
        {
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

            if (hits.Count() > 0)
            {
                gameObject.SetActive(false);
            }

            foreach (RaycastHit2D hit in hits)
            {
                timer = 0;
            }

            transform.position = newPosition;
        }

        public virtual void FixedUpdate()
        {
            if (timer >= Stats.duration)
            {
                timer = 0;
                gameObject.SetActive(false);
            }

            timer++;
        }
    }
}
