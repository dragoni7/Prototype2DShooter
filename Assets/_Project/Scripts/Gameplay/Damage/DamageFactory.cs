namespace dragoni7
{
    public static class DamageFactory
    {
        /// <summary>
        /// Returns a damage strategy from a damage type enum
        /// </summary>
        /// <param name="type">damage type to get</param>
        /// <returns>new damage strategy</returns>
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
