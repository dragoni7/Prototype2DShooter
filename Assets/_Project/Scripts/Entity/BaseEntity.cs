using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class BaseEntity : MonoBehaviour
    {
        public Rigidbody2D rb;
        public List<AbstractAbility> Abilities { get; set; }

        [SerializeField]private Transform equipParent;
        public Transform EquipParent => equipParent;

        public bool canMove;
        public bool canAttack;
        public virtual void TakeDamage(int damage)
        {

        }

        public virtual void Move(Vector3 moveThisFrame)
        {

        }
    }
}
