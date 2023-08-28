using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace dragoni7
{
    /// <summary>
    /// Stores a collection of attributes
    /// </summary>
    [Serializable]
    public class Attributes : ICloneable
    {
        [SerializeField]
        private List<AttributeInfo> _attributeInfo;
        private Dictionary<AttributeType, float> _attributes;

        public void Initialize()
        {
            _attributes = _attributeInfo.ToDictionary(a => a.attributeType, a => a.attributeValue);
        }

        public float Get(AttributeType attribute)
        {
            if (_attributes.TryGetValue(attribute, out float value))
            {
                return value;
            }
            else
            {
                //Debug.LogError($"No attribute value found for {attribute} on {this}");
                return 0f;
            }
        }

        public void Set(AttributeType attribute, float newValue)
        {
            if (_attributes.ContainsKey(attribute))
            {
                _attributes[attribute] = newValue;
            }
            else
            {
                _attributes.Add(attribute, newValue);
            }
        }

        public object Clone()
        {
            Attributes copy = (Attributes)this.MemberwiseClone();
            copy._attributes = new Dictionary<AttributeType, float>(_attributes);
            copy._attributeInfo = new List<AttributeInfo>(_attributeInfo);
            return copy;
        }
    }
}
