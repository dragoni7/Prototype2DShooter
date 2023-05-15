using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class AIData : MonoBehaviour
    {
        public List<Transform> targets = null;
        public Collider2D[] obstacles = null;

        public Transform currentTarget;
        public int GetTargetsCount() => targets == null ? 0 : targets.Count;
    }
}
