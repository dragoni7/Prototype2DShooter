using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class ScriptableEntity : AbstractData
    {
        [SerializeField, SerializeReference] private List<AbstractAbility> _abilities;
        public List<AbstractAbility> Abilities => _abilities;

        [SerializeField] private EntityAttributes _baseAttributes;
        public EntityAttributes BaseAttributes => _baseAttributes;
    }
}
