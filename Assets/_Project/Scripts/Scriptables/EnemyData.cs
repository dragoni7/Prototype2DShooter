using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Enemy")]
    public class EnemyData : EntityData
    {
        public AbstractEnemy enemyPrefab;
        public EmitterData scriptableEmitter;
        public AbstractBrain enemyAiPrefab;
    }
}
