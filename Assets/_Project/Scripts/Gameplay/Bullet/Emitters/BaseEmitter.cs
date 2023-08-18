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
        public EmitterAttributes attributes { get; protected set; }

        protected int timer = 0;
        protected bool canEmit = true;

        public void SetAttributes(EmitterAttributes attributes)
        {
            this.attributes = attributes;
        }

        public virtual void TryEmitBullets(DamageModifiers damageModifier)
        {
            if (canEmit)
            {
                foreach (Transform point in Pattern.points)
                {
                    BulletController.Instance.SpawnBullet(Bullet, point.position, point.rotation, point.up, attributes.bulletForce, damageModifier);
                }

                canEmit = false;
            }
        }

        public virtual void FixedUpdate()
        {
            if (timer >= attributes.emitTime)
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
