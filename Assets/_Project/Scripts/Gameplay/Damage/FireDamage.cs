using UnityEngine;

namespace dragoni7
{
    public class FireDamage : IDamage
    {
        public Color Color => Color.red;

        public Color GetColor() => Color;
        public float PerformDamage(Attributes attributes, Entity target)
        {
            // do fire stuff
            float fireDamage = 1 + attributes.Get(AttributeType.FireDamage);
            float damageAfterResist = fireDamage - (target.CurrentAttributes.Get(AttributeType.FireResistance) * fireDamage);
            target.TakeDamage(damageAfterResist);

            return damageAfterResist;
        }
    }
}
