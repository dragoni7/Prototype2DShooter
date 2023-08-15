using UnityEngine;

namespace dragoni7
{
    public class FireDamage : IDamage
    {
        public Color Color => Color.red;

        public Color GetColor() => Color;
        public float PerformDamage(DamageModifiers damageModifier, Entity target)
        {
            // do fire stuff
            float fireDamage = 1 + damageModifier.fireModifier;
            float damageAfterResist = fireDamage - (target.Attributes.damageResistances.fireResistance * fireDamage);
            target.TakeDamage(damageAfterResist);

            return damageAfterResist;
        }
    }
}
