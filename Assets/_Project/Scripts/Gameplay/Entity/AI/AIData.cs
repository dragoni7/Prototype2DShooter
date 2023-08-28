using System;
using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    /// <summary>
    /// Stores shared data for behaviour tree
    /// </summary>
    public class AIData : MonoBehaviour
    {
        /// <summary>
        /// Entity this data resides on
        /// </summary>
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
                GameEventManager.Instance.EventBus.Raise(new EntityMoveEvent { entity = entity, moveThisFrame = _movementInput });
            }
        }

        public Vector3 attackDirection;
        public bool following = false;
        public List<AbstractSteeringBehaviour> behaviours;
        public int GetTargetsCount() => targets == null ? 0 : targets.Count;
    }
}
