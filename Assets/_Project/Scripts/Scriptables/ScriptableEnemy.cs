using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Enemy")]
    public class ScriptableEnemy : ScriptableEntity
    {
        public AbstractEnemy enemyPrefab;
        public ScriptableEmitter scriptableEmitter;
        public AbstractBrain enemyAiPrefab;
    }
}
