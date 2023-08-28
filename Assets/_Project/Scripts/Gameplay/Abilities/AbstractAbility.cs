using System.Collections;
using UnityEngine;

namespace dragoni7
{
    /// <summary>
    /// Abstract class for abilities. Must be abstract for serialization
    /// </summary>
    public abstract class AbstractAbility : MonoBehaviour
    {
        /// <summary>
        /// Executes the ability
        /// </summary>
        /// <param name="entity">The entity to perform the ability</param>
        /// <returns></returns>
        public abstract IEnumerator Execute(Entity entity);
    }
}
