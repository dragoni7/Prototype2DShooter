using System.Collections.Generic;
using UnityEngine;
using Util;
using Utils;

namespace dragoni7
{
    public static class WallGenerator
    {
        public static void CreateWalls(HashSet<Vector2Int> roomFloor, TilemapVisualizer tilemapVisualizer)
        {
            HashSet<Vector2Int> wallPositions = FindWallsInDirections(roomFloor, DirectionHelper.cardinalDirections);

            foreach (Vector2Int wallPosition in wallPositions)
            {
                tilemapVisualizer.PaintSingleBasicWall(wallPosition);
            }
        }

        public static void CreateRoomWalls(List<IBounds> rooms, TilemapVisualizer tilemapVisualizer)
        {
            foreach (IBounds room in rooms)
            {
                Vector2Int position = (Vector2Int)room.Min();

                for (int i = 0; i < room.Scale().x; i++)
                {
                    tilemapVisualizer.PaintSingleBasicWall(position + (Vector2Int.right * i));
                }

                for (int i = 0; i < room.Scale().y; i++)
                {
                    tilemapVisualizer.PaintSingleBasicWall(position + (Vector2Int.up * i));
                }

                position = (Vector2Int)room.Max();

                for (int i = 0; i < room.Scale().x; i++)
                {
                    tilemapVisualizer.PaintSingleBasicWall(position + (Vector2Int.left * i));
                }

                for (int i = 0; i < room.Scale().y; i++)
                {
                    tilemapVisualizer.PaintSingleBasicWall(position + (Vector2Int.down * i));
                }
            }
        }

        public static void CreateDoors(List<Room> rooms, HashSet<Vector2Int> corridorFloor, float chance, TilemapVisualizer tilemapVisualizer)
        {
            foreach (Room room in rooms)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int x = 0; x < room.Bounds.Scale().x; x++)
                    {
                        Vector2Int position = (Vector2Int)room.Bounds.Min() + (Vector2Int.right * x) + Vector2Int.down;

                        if (corridorFloor.Contains(position))
                        {
                            // place door here
                            tilemapVisualizer.PaintSingleBasicWall(position);
                        }
                    }
                }
            }
        }

        private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
        {
            HashSet<Vector2Int> wallPositions = new();

            foreach (Vector2Int position in floorPositions)
            {
                foreach (Vector2Int direction in directions)
                {
                    Vector2Int neighbourPosition = position + direction;

                    if (!floorPositions.Contains(neighbourPosition))
                    {
                        wallPositions.Add(position);
                    }
                }
            }

            return wallPositions;
        }
    }
}
