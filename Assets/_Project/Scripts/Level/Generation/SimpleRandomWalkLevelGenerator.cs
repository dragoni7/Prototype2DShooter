using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace dragoni7
{
    public class SimpleRandomWalkLevelGenerator : AbstractLevelGenerator
    {
        [SerializeField] 
        protected SimpleRandomWalkData randomWalkParams;
        protected override void RunProceduralGeneration()
        {
            HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParams, startPosition);
            tilemapVisualizer.Clear();
            tilemapVisualizer.PaintFloorTiles(floorPositions); // paint floor
            WallGenerator.CreateWalls(floorPositions, tilemapVisualizer); // paint walls
        }

        protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData parameters, Vector2Int position)
        {
            var currentPos = position;
            HashSet<Vector2Int> floorPositions = new();

            for (int i = 0; i < parameters.iterations; i++)
            {
                var path = ProceduralGenAlgorithms.SimpleRandomWalk(currentPos, parameters.walklength);
                floorPositions.UnionWith(path);

                if (parameters.startRandomlyEachIteration)
                {
                    currentPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
                }
            }

            return floorPositions;
        }
    }
}
