using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable LootTable")]
    public class LootTableData : ScriptableObject
    {
        public List<Drop> table;
        private int _totalWeight = -1;
        public int TotalWeight
        {
            get
            {
                if (_totalWeight == -1)
                {
                    CalculateTotalWeight();
                }
                return _totalWeight;
            }
        }

        private void CalculateTotalWeight()
        {
            _totalWeight = 0;
            for (int i = 0; i < table.Count; i++)
            {
                _totalWeight += table[i].weight;
            }
        }

        public ItemData GetDrop()
        {
            int roll = Random.Range(0, TotalWeight);

            for (int i = 0; i < table.Count; i++)
            {
                roll -= table[i].weight;

                if (roll < 0)
                {
                    return table[i].drop;
                }
            }

            return table[0].drop;
        }
    }

    [Serializable]
    public struct Drop
    {
        public ItemData drop;
        public int weight;
    }
}
