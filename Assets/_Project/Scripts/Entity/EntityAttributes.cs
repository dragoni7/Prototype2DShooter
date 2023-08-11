using System;

namespace dragoni7
{
    [Serializable]
    public struct EntityAttributes
    {
        public DamageModifiers damageModifiers;
        public DamageResistances damageResistances;
        public float health;
        public float speed;
        public float shootingSpeed;
    }
}
