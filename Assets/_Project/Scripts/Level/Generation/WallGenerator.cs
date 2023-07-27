using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace dragoni7
{
    public static class WallGenerator
    {
        public static void CreateWalls(HashSet<Vector2Int> roomFloor, HashSet<Vector2Int> corridorFloor, TilemapVisualizer tilemapVisualizer)
        {
            HashSet<Vector2Int> wallPositions = FindWallsInDirections(roomFloor, DirectionHelper.cardinalDirections);

            foreach (Vector2Int wallPosition in wallPositions)
            {
                if (!roomFloor.Contains(wallPosition))
                {
                    tilemapVisualizer.PaintSingleBasicWall(wallPosition);
                }
            }
        }

        public static void CreateDoors(List<Room> rooms, float chance)
        {

        }

        private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
        {
            HashSet<Vector2Int> wallPositions = new();

            foreach (var position in floorPositions)
            {
                foreach (var direction in directions)
                {
                    var neighbourPosition = position + direction;
                    if (!floorPositions.Contains(neighbourPosition))
                    {
                        wallPositions.Add(neighbourPosition);
                    }
                }
            }

            return wallPositions;
        }
    }
}
