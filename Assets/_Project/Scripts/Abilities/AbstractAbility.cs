using System.Collections;
using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractAbility : MonoBehaviour
    {
        public abstract IEnumerator Execute(BaseEntity subject);
    }
}
