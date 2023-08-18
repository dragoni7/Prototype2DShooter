using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public Item prefab;
        public Sprite sprite;
    }
}
