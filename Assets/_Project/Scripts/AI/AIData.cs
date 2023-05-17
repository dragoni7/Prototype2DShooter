using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class AIData : MonoBehaviour
    {
        public List<Transform> targets = null;
        public Collider2D[] obstacles = null;

        public Rigidbody2D rb;

        public Transform currentTarget;
        public Vector2 movementInput;
        public Vector3 attackDirection;
        public bool following = false;
        public SteeringContextSolver movementDirectionSolver;
        public List<AbstractSteeringBehaviour> behaviours;

        public void Awake()
        {
            rb = GetComponentInParent<Rigidbody2D>();
        }
        public int GetTargetsCount() => targets == null ? 0 : targets.Count;
    }
}
