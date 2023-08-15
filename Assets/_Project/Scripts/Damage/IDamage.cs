using UnityEngine;

namespace dragoni7
{
    public interface IDamage
    {
        public Color GetColor();
        public float PerformDamage(DamageModifiers damageModifier, Entity target);
    }
}
