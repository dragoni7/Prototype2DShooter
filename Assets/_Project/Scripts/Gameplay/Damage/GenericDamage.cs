using UnityEngine;

namespace dragoni7
{
    public class GenericDamage : IDamage
    {
        public static Color Color => Color.gray;
        public Color GetColor() => Color;
        public float PerformDamage(Attributes attributes, Entity target)
        {
            float genericDamage = 1 + attributes.Get(AttributeType.GenericDamage);
            float damageAfterResist = genericDamage - (target.CurrentAttributes.Get(AttributeType.GenericResistance) * genericDamage);

            target.TakeDamage(damageAfterResist);
            return damageAfterResist;
        }
    }
}
