using UnityEngine;
using static dragoni7.EmitterData;

namespace dragoni7
{
    public class BaseEmitter : MonoBehaviour
    {
        public BasePattern pattern;
        public BulletData Bullet { get; set; }
        public EmitterStats Stats { get; protected set; }

        protected int timer = 0;
        protected bool canEmit = true;

        public void SetStats(EmitterStats stats)
        {
            Stats = stats;
        }

        public virtual void TryEmitBullets()
        {
            if (canEmit)
            {
                foreach (Vector2 point in pattern.points)
                {
                    BulletController.Instance.SpawnBullet(Bullet, transform.position + (Vector3)point, transform.rotation, transform.up, Stats.bulletForce);
                }

                canEmit = false;
            }
        }

        public virtual void FixedUpdate()
        {
            if (timer >= Stats.emitTime)
            {
                canEmit = true;
                timer = 0;
            }
            else if (!canEmit)
            {
                timer++;
            }
        }
    }
}
