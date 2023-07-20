using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static dragoni7.BulletData;

namespace dragoni7
{
    public class AbstractBullet : MonoBehaviour
    {
        public Vector2 Velocity { get; set; }
        public float BulletForce { get; set; }
        public BulletStats Stats { get; protected set; }

        [SerializeField] private string _ignoreTag;

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

                if (!hitObject.CompareTag(_ignoreTag))
                {
                    // hit logic
                    if (gameObject.activeSelf)
                    {
                        _timer = 0;
                        gameObject.SetActive(false);
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
