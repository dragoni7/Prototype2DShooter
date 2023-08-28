using UnityEngine;
using static dragoni7.EmitterData;

namespace dragoni7
{
    public class BaseEmitter : MonoBehaviour
    {
        private BasePattern _pattern;
        public BasePattern Pattern
        {
            get { return _pattern; }
            set
            {
                _pattern = value;
                _pattern.transform.SetParent(transform);
            }
        }
        public BulletData Bullet { get; set; }
        public EmitterAttributes Attributes { get; protected set; }

        protected int timer = 0;
        protected bool canEmit = true;

        public void SetAttributes(EmitterAttributes attributes)
        {
            this.Attributes = attributes;
        }

        public virtual void TryEmitBullets(Attributes attributes)
        {
            if (canEmit)
            {
                foreach (Transform point in Pattern.points)
                {
                    BulletController.Instance.SpawnBullet(Bullet, point.position, point.rotation, point.up, Attributes.bulletForce, attributes);
                }

                canEmit = false;
            }
        }

        public virtual void FixedUpdate()
        {
            if (timer >= Attributes.emitTime)
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
