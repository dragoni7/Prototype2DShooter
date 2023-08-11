using UnityEngine;

namespace dragoni7
{
    public class GenericDamage : IDamage
    {
        public static Color Color => Color.gray;
        public Color GetColor() => Color;
        public void PerformDamage(DamageModifiers damageModifier, Entity target)
        {
            float genericDamage = 1 + damageModifier.genericModifier;
            float damageAfterResist = genericDamage - (target.Attributes.damageResistances.genericResistance * genericDamage);

            target.TakeDamage(damageAfterResist);
        }
    }
}
