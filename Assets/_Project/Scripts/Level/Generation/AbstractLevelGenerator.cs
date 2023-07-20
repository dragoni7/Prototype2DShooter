using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractLevelGenerator : MonoBehaviour
    {
        [SerializeField] protected TilemapVisualizer tilemapVisualizer = null;

        [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

        public void GenerateLevel()
        {
            tilemapVisualizer.Clear();
            RunProceduralGeneration();
        }

        protected abstract void RunProceduralGeneration();
    }
}
