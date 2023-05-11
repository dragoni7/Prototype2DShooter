using UnityEngine;
using static dragoni7.ScriptableEmitter;

namespace dragoni7
{
    public class BaseEmitter : MonoBehaviour
    {
        public BasePattern pattern;
        public int emitTime;
        public ScriptableBullet Bullet { get; set; }
        public EmitterStats Stats { get; protected set; }

        protected int timer = 0;
        protected bool canEmit = true;

        public void SetStats(EmitterStats stats)
        {
            Stats = stats;
        }

        public void EmitBullets()
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
            if (timer >= emitTime)
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
