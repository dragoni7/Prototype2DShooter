using UnityEngine;

namespace dragoni7
{
    public interface IDamage
    {
        public Color GetColor();
        public void PerformDamage(DamageModifiers damageModifier, Entity target);
    }
}
