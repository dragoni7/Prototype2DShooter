using UnityEngine;

namespace dragoni7
{
    public interface IDamage
    {
        /// <summary>
        /// Gets the color associated with the damage type
        /// </summary>
        /// <returns>damage color</returns>
        public Color GetColor();

        /// <summary>
        /// Determines amount of damage and inflicts it on an entity
        /// </summary>
        /// <param name="attributes">Attributes of the attacking entity. Used to calculate damage modifiers</param>
        /// <param name="target">Entity to take damage</param>
        /// <returns>Damage taken by entity</returns>
        public float PerformDamage(Attributes attributes, Entity target);
    }
}
