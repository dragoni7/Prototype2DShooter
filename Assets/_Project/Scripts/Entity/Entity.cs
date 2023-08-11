using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public abstract class Entity : MonoBehaviour
    {
        public Rigidbody2D rb;
        public List<AbstractAbility> Abilities { get; set; }

        protected EntityAttributes _attributes;
        public EntityAttributes Attributes => _attributes;

        [SerializeField]
        private Transform _equipParent;
        public Transform EquipParent => _equipParent;
        public bool CanMove { get; set; }
        public bool CanAttack { get; set; }
        public void SetAttributes(EntityAttributes stats)
        {
            _attributes = stats;
        }
        public virtual void TakeDamage(float damage)
        {

        }
    }
}
