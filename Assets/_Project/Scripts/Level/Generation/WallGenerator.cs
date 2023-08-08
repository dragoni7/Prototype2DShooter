using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Util;
using WUG.BehaviorTreeVisualizer;

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
        public static void CreateRoomWalls(List<IBounds> rooms, HashSet<Vector2Int> corridor, TilemapVisualizer tilemapVisualizer, int corridorSize, float openRoomChance)
        {
            HashSet<Vector2Int> walls = new();
            List<Vector2Int> doors = new();

            foreach (IBounds room in rooms)
            {
                walls.UnionWith(GetWallPositions(room, corridor));

                if (Random.value > openRoomChance)
                {
                    walls.UnionWith(GetWallPositions(room, corridor));
                }
                else
                {
                    HashSet<Vector2Int> roomWalls;
                    List<Vector2Int> roomDoors;
                    (roomWalls, roomDoors) = GetWallAndDoorPositions(room, corridor, corridorSize);

                    walls.UnionWith(roomWalls);
                    doors.AddRange(roomDoors);
                }
            }

            foreach (Vector2Int position in walls)
            {
                tilemapVisualizer.PaintSingleBasicWall(position);
            }

            foreach (Vector2Int position in doors)
            {
                tilemapVisualizer.PaintSingleBasicDoor(position);
            }
        }
        private static HashSet<Vector2Int> GetWallPositions(IBounds room, HashSet<Vector2Int> corridor)
        {
            HashSet<Vector2Int> wallPositions = new();

            foreach (Vector2Int position in room.EdgePositions())
            {
                // if position is not contained in corridors, create wall
                if (!corridor.Contains(position))
                {
                    wallPositions.Add(position);
                }
            }

            return wallPositions;
        }
        private static (HashSet<Vector2Int>, List<Vector2Int>) GetWallAndDoorPositions(IBounds room, HashSet<Vector2Int> corridor, int corridorSize)
        {
            HashSet<Vector2Int> wallPositions = new();
            List<Vector2Int> potentialDoorways = new();
            List<Vector2Int> doorways = new();

            foreach (Vector2Int position in room.EdgePositions())
            {
                // if position is not contained in corridors, create wall
                if (!corridor.Contains(position))
                {
                    wallPositions.Add(position);
                }
                // otherwise, add position to potential door positions
                // vertical doorway
                else if ((room.PositionsWithin().Contains(position + Vector2Int.up) && !room.EdgePositions().Contains(position + Vector2Int.up) && corridor.Contains(position + Vector2Int.down)) ||
                        (room.PositionsWithin().Contains(position + Vector2Int.down) && !room.EdgePositions().Contains(position + Vector2Int.down) && corridor.Contains(position + Vector2Int.up)) &&
                        (room.EdgePositions().Contains(position + Vector2Int.left) && room.EdgePositions().Contains(position + Vector2Int.right)))
                {
                    potentialDoorways.Add(position);
                }
                // horizontal doorway
                else if ((room.PositionsWithin().Contains(position + Vector2Int.left) && !room.EdgePositions().Contains(position + Vector2Int.left) && corridor.Contains(position + Vector2Int.right)) ||
                    (room.PositionsWithin().Contains(position + Vector2Int.right) && !room.EdgePositions().Contains(position + Vector2Int.right) && corridor.Contains(position + Vector2Int.left)) &&
                    (room.EdgePositions().Contains(position + Vector2Int.up) && room.EdgePositions().Contains(position + Vector2Int.down)))
                {
                    potentialDoorways.Add(position);
                }
                else
                {
                    wallPositions.Add(position);
                }
            }

            potentialDoorways.Shuffle();

            // only select doorways at different edges
            potentialDoorways.ForEach(pos => {
                if (doorways.TrueForAll(p => (p.x != pos.x && p.y != pos.y) || Vector2Int.Distance(p, pos) >= corridorSize))
                {
                    doorways.Add(pos);
                }
            });

            wallPositions.AddRange(potentialDoorways.Except(doorways));

            return (wallPositions, doorways);
        }
    }
}
