namespace dragoni7
{
    public static class DamageFactory
    {
        public static IDamage GetDamage(DamageTypes type)
        {
            switch(type)
            {
                case DamageTypes.Generic:
                    return new GenericDamage();
                case DamageTypes.Fire:
                    return new FireDamage();
                default:
                    return null;
            }
        }
    }
}
