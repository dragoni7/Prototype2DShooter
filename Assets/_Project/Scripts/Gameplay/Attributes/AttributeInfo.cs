using System;

namespace dragoni7
{
    /// <summary>
    /// Stores a pair of attribute type and it's value
    /// </summary>
    [Serializable]
    public struct AttributeInfo
    {
        public AttributeType attributeType;
        public float attributeValue;
    }
}
