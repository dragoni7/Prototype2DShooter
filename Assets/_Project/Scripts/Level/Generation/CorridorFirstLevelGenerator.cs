using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public class CorridorFirstLevelGenerator : SimpleRandomWalkLevelGenerator
    {
        [SerializeField]
        private int corridorLength = 14, corridorCount = 5;

        [SerializeField]
        [Range(0.1f, 1)]
        private float roomPercent = 0.8f;
        protected override void RunProceduralGeneration()
        {
            CorridorFirstLevelGeneration();
        }

        private void CorridorFirstLevelGeneration()
        {
            HashSet<Vector2Int> floorPositions = new();
            HashSet<Vector2Int> potentialRoomPositions = new();

            List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomPositions);

            HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

            List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

            CreateRoomAtDeadEnd(deadEnds,roomPositions);

            floorPositions.UnionWith(roomPositions); // combine corridor and room floor positions

            for (int i = 0; i < corridors.Count; i++)
            {
                //corridors[i] = IncreaseCorridorSizeByOne(corridors[i]);
                corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
                floorPositions.UnionWith(corridors[i]);
            }

            tilemapVisualizer.PaintFloorTiles(floorPositions);
            //WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
        }

        private List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor)
        {
            List<Vector2Int> newCorridor = new();

            for (int i = 1; i < corridor.Count; i++)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                    }
                }
            }

            return newCorridor;
        }

        private List<Vector2Int> IncreaseCorridorSizeByOne(List<Vector2Int> corridor)
        {
            List<Vector2Int> newCorridor = new List<Vector2Int>();
            Vector2Int previousDirection = Vector2Int.zero;

            for (int i = 1; i < corridor.Count; i++)
            {
                Vector2Int directionFromCell = corridor[i] - corridor[i - 1];

                // handle corners
                if (previousDirection != Vector2Int.zero && directionFromCell != previousDirection)
                {
                    
                    for (int x = -1; x < 2; x++)
                    {
                        for (int y = -1; y < 2; y++)
                        {
                            newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                        }
                    }

                    previousDirection = directionFromCell;
                }
                else
                {
                    // add a single cell in direction + 90 degrees
                    Vector2Int newCorridorTileOffset = GetDirection90From(directionFromCell);
                    newCorridor.Add(corridor[i - 1]);
                    newCorridor.Add(corridor[i - 1] + newCorridorTileOffset);
                }
            }

            return newCorridor;
        }

        private Vector2Int GetDirection90From(Vector2Int direction)
        {
            if (direction == Vector2Int.up)
                return Vector2Int.right;
            if (direction == Vector2Int.right)
                return Vector2Int.down;
            if (direction == Vector2Int.down)
                return Vector2Int.left;
            if (direction == Vector2Int.left)
                return Vector2Int.up;
            return Vector2Int.zero;
        }

        private void CreateRoomAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
        {
            foreach(var position in deadEnds)
            {
                if (!roomFloors.Contains(position))
                {
                    var room = RunRandomWalk(randomWalkParams, position);
                    roomFloors.UnionWith(room);
                }
            }
        }

        private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
        {
            List<Vector2Int> deadEnds = new();

            foreach (var position in floorPositions)
            {
                int neighborsCount = 0;

                // check for neighbors in each direction. Dead ends only have 1 neighbor

                foreach(var direction in DirectionHelper.cardinalDirections)
                {
                    if (floorPositions.Contains(position + direction))
                    {
                        neighborsCount++;
                    }
                }

                if (neighborsCount == 1)
                {
                    deadEnds.Add(position);
                }
            }

            return deadEnds;
        }

        private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
        {
            HashSet<Vector2Int> roomPositions = new();
            int roomCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

            List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomCount).ToList();

            foreach (var roomPosition in roomsToCreate)
            {
                var roomFloor = RunRandomWalk(randomWalkParams, roomPosition);
                roomPositions.UnionWith(roomFloor);
            }

            return roomPositions;
        }

        private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
        {
            var currentPosition = startPosition;
            potentialRoomPositions.Add(currentPosition);
            List<List<Vector2Int>> corridors = new();

            for (int i = 0; i < corridorCount; i++)
            {
                var corridor = ProceduralGenAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
                corridors.Add(corridor);
                currentPosition = corridor[corridor.Count - 1];
                potentialRoomPositions.Add(currentPosition);
                floorPositions.UnionWith(corridor);
            }

            return corridors;
        }
    }
}
