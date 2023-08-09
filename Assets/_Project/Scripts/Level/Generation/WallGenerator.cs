using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
                    Vector2Int neighborPosition = position + direction;

                    if (!corridorFloor.Contains(neighborPosition) && !roomFloor.Contains(neighborPosition))
                    {
                        tilemapVisualizer.PaintSingleBasicWall(neighborPosition);
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
                    (roomWalls, roomDoors) = GetWallAndDoorPositions(room, corridor, corridorSize + 2);

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

            wallPositions.AddRange(room.Corners());

            foreach (Vector2Int position in room.EdgePositions().Except(room.Corners()))
            {
                // if position is not contained in corridors, create wall
                if (!corridor.Contains(position))
                {
                    wallPositions.Add(position);
                }
                // otherwise it is a potential door location
                else
                {
                    bool doorway = true;

                    foreach (Vector2Int direction in DirectionHelper.cardinalDirections)
                    {
                        if (!corridor.Contains(position + direction) && !room.PositionsWithin().Contains(position + direction))
                        {
                            wallPositions.Add(position);
                            doorway = false;
                            break;
                        }
                    }

                    if (doorway)
                    {
                        potentialDoorways.Add(position);
                    }
                }
            }

            potentialDoorways.Shuffle();

            // only select doorways at different edges or within corridor distance
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
