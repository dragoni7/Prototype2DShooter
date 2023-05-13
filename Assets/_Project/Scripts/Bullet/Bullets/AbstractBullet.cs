using System.Linq;
using Unity.VisualScripting;
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
            Vector2 newPosition = currentPosition + BulletForce * Time.deltaTime * Velocity;

            RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

            for (int i = 0; i < hits.Count(); i++)
            {
                GameObject hitObject = hits[i].collider.gameObject;

                if (!hitObject.CompareTag("Player"))
                {
                    // hit logic
                    if (gameObject.activeSelf)
                    {
                        timer = 0;
                        gameObject.SetActive(false);
                    }
                }
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
