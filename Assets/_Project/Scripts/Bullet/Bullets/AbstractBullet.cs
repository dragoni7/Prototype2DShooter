using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static dragoni7.BulletData;

namespace dragoni7
{
    public class AbstractBullet : MonoBehaviour
    {
        public Vector2 Velocity { get; set; }
        public float BulletForce { get; set; }
        public BulletStats Stats { get; protected set; }

        [SerializeField] private List<string> _ignoreTags;

        private int _timer;

        public virtual void Start()
        {
            _timer = 0;
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

                foreach (string tag in _ignoreTags)
                {
                    if (!hitObject.CompareTag(tag))
                    {
                        // hit logic
                        if (gameObject.activeSelf)
                        {
                            _timer = 0;
                            gameObject.SetActive(false);
                            hitObject.GetComponent<IDestructable>()?.TakeDamage(Stats.damage);
                        }
                    }
                }
            }

            transform.position = newPosition;
        }

        public virtual void FixedUpdate()
        {
            if (_timer >= Stats.duration)
            {
                _timer = 0;
                gameObject.SetActive(false);
            }

            _timer++;
        }
    }
}
