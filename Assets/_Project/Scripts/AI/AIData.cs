using System;
using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class AIData : MonoBehaviour
    {
        public Entity entity;
        public List<Transform> targets = null;
        public Collider2D[] obstacles = null;

        public Transform currentTarget;
        public Vector2 origin;

        private Vector2 _movementInput;
        public Vector2 MovementInput
        {
            get { return _movementInput; }
            set {
                _movementInput = value;
                EventSystem.Instance.TriggerEvent(Events.OnEntityMove, new Dictionary<string, object> { { "entity", entity }, { "moveThisFrame", _movementInput } });
            }
        }

        public Vector3 attackDirection;
        public bool following = false;
        public List<AbstractSteeringBehaviour> behaviours;

        public Action<Vector3> OnAttack;
        public int GetTargetsCount() => targets == null ? 0 : targets.Count;
        public void Start()
        {
            
        }
    }
}
