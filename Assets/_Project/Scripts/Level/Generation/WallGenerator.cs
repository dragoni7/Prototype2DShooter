using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace dragoni7
{
    public static class WallGenerator
    {
        public static void CreateCorridorWalls(HashSet<Vector2Int> corridorFloor, HashSet<Vector2Int> roomFloor, TilemapVisualizer tilemapVisualizer)
        {
            foreach (Vector2Int position in corridorFloor)
            {
                foreach (Vector2Int direction in DirectionHelper.cardinalDirections)
                {
                    Vector2Int neighbourPosition = position + direction;

                    if (!corridorFloor.Contains(neighbourPosition))
                    {
                        if (!roomFloor.Contains(neighbourPosition))
                        {
                            tilemapVisualizer.PaintSingleBasicWall(neighbourPosition);
                        }
                    }
                }
            }
        }
        public static void CreateRoomWallsAndDoors(List<IBounds> rooms, HashSet<Vector2Int> corridor, TilemapVisualizer tilemapVisualizer)
        {
            HashSet<Vector2Int> wallPositions = new();

            foreach (IBounds room in rooms)
            {
                int addedDoors = 0;
                List<Vector2Int> doorPositions = new();
                foreach (Vector2Int position in room.EdgePositions())
                {
                    // if position is not contained in corridors, create wall
                    if (!corridor.Contains(position))
                    {
                        wallPositions.Add(position);
                    }
                    // otherwise, create walls with a door
                    else if (addedDoors < room.Corners().Count)
                    {
                        if (doorPositions.Any(p => p.x == position.x || p.y == position.y))
                        {
                            wallPositions.Add(position);
                        }
                        // vertical doorway
                        else if ((room.PositionsWithin().Contains(position + Vector2Int.up) && !room.EdgePositions().Contains(position + Vector2Int.up) && corridor.Contains(position + Vector2Int.down)) ||
                            (room.PositionsWithin().Contains(position + Vector2Int.down) && !room.EdgePositions().Contains(position + Vector2Int.down) && corridor.Contains(position + Vector2Int.up)) &&
                            (room.EdgePositions().Contains(position + Vector2Int.left) && room.EdgePositions().Contains(position + Vector2Int.right)))
                        {
                            doorPositions.Add(position);
                            addedDoors++;
                        }
                        // horizontal doorway
                        else if ((room.PositionsWithin().Contains(position + Vector2Int.left) && !room.EdgePositions().Contains(position + Vector2Int.left) && corridor.Contains(position + Vector2Int.right)) ||
                            (room.PositionsWithin().Contains(position + Vector2Int.right) && !room.EdgePositions().Contains(position + Vector2Int.right) && corridor.Contains(position + Vector2Int.left)) &&
                            (room.EdgePositions().Contains(position + Vector2Int.up) && room.EdgePositions().Contains(position + Vector2Int.down)))
                        {
                            doorPositions.Add(position);
                            addedDoors++;
                        }
                        else
                        {
                            wallPositions.Add(position);
                        }
                    }
                    else
                    {
                        tilemapVisualizer.PaintSingleBasicWall(position);
                    }
                }

                foreach (Vector2Int doorPosition in doorPositions)
                {
                    tilemapVisualizer.PaintSingleBasicDoor(doorPosition);
                }
            }

            foreach(Vector2Int wallPosition in wallPositions)
            {
                tilemapVisualizer.PaintSingleBasicWall(wallPosition);
            }
        }
    }
}
