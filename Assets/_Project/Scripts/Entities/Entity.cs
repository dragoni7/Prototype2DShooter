using UnityEngine;
using static dragoni7.AbstractScriptableEntity;

namespace dragoni7
{
    public class Entity : MonoBehaviour
    {
        public Stats Stats { get; protected set; }
        public void SetStats(Stats stats)
        {
            Stats = stats;
        }

        public virtual void TakeDamage(int damage)
        {

        }
    }
}
