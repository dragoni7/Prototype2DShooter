using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class EntityData : ScriptableObject
    {
        [SerializeField, SerializeReference] 
        private List<AbstractAbility> _abilities;
        public List<AbstractAbility> Abilities => _abilities;

        [SerializeField] 
        private Attributes _baseAttributes;
        public Attributes BaseAttributes => _baseAttributes;
    }
}
