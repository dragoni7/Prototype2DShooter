using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static dragoni7.ScriptableEmitter;

namespace dragoni7
{
    public class BaseEmitter : MonoBehaviour
    {
        public BasePattern pattern;
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

        public void UpdatePosition(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
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
