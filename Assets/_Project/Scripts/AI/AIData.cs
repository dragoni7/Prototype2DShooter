using System;
using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class AIData : MonoBehaviour
    {
        public List<Transform> targets = null;
        public Collider2D[] obstacles = null;

        public Transform currentTarget;
        public Vector2 origin;
        public Vector2 movementInput;
        public Vector3 attackDirection;
        public bool following = false;
        public List<AbstractSteeringBehaviour> behaviours;

        public Action<Vector3> OnMove;
        public Action<Vector3> OnAttack;
        public int GetTargetsCount() => targets == null ? 0 : targets.Count;

        public void Start()
        {
            
        }
    }
}
