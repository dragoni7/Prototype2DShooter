using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace dragoni7
{
    public class RoomFirstLevelGenerator : SimpleRandomWalkLevelGenerator
    {
        [SerializeField]
        private int minRoomWidth = 4, minRoomHeight = 4;
        [SerializeField]
        private int levelWidth = 20, levelHeight = 20;
        [SerializeField]
        [Range(0,10)]
        private int offset = 1;
        [SerializeField]
        private bool randomWalkRooms = false;

        protected override void RunProceduralGeneration()
        {
            CreateRooms();
        }

        private void CreateRooms()
        {
            var roomsList = ProceduralGenAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int) startPosition, new Vector3Int(levelWidth, levelHeight, 0)), minRoomWidth, minRoomHeight);

            HashSet<Vector2Int> floor = new();

            if (randomWalkRooms)
            {
                floor = CreateRoomsRandomly(roomsList);
            }
            else
            {
                floor = CreateSimpleRooms(roomsList);
            }

            List<Vector2Int> roomCenters = new();
            foreach (var room in roomsList)
            {
                roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
            }

            HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
            floor.UnionWith(corridors);

            tilemapVisualizer.PaintFloorTiles(floor);
            //WallGenerator.CreateWalls(floor, tilemapVisualizer);
        }

        private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
        {
            HashSet<Vector2Int> floor = new();

            for(int i = 0; i < roomsList.Count; i++)
            {
                var roomBounds = roomsList[i];
                var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
                var roomFloor = RunRandomWalk(randomWalkParams, roomCenter);

                foreach (var position in roomFloor)
                {
                    if (position.x > (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) &&
                        position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                    {
                        floor.Add(position);
                    }
                }
            }

            return floor;
        }

        private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
        {
            HashSet<Vector2Int> corridors = new();
            var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
            roomCenters.Remove(currentRoomCenter);

            while (roomCenters.Count > 0)
            {
                Vector2Int closets = FindClosetsPointTo(currentRoomCenter, roomCenters);
                roomCenters.Remove(closets);
                HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closets);
                currentRoomCenter = closets;
                corridors.UnionWith(newCorridor);
            }

            return corridors;
        }

        private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
        {
            HashSet<Vector2Int> corridor = new();
            var position = currentRoomCenter;
            corridor.Add(position);

            while(position.y != destination.y)
            {
                if (destination.y > position.y)
                {
                    position += Vector2Int.up;
                }
                else if (destination.y < position.y)
                {
                    position += Vector2Int.down;
                }

                corridor.Add(position);
                var extraWidth = position;
                corridor.Add((extraWidth + Vector2Int.right));
                corridor.Add((extraWidth + Vector2Int.left));
            }

            while (position.x != destination.x)
            {
                if (destination.x > position.x)
                {
                    position += Vector2Int.right;
                }
                else if (destination.x < position.x)
                {
                    position += Vector2Int.left;
                }

                corridor.Add(position);
                var extraWidth = position;
                corridor.Add((extraWidth + Vector2Int.up));
                corridor.Add((extraWidth + Vector2Int.down));
            }

            return corridor;
        }

        private Vector2Int FindClosetsPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
        {
            Vector2Int closest = Vector2Int.zero;
            float distance = float.MaxValue;
            foreach (var position in roomCenters)
            {
                float currentDistance = Vector2.Distance(position, currentRoomCenter);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closest = position;
                }
            }

            return closest;
        }

        private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
        {
            HashSet<Vector2Int> floor = new();

            foreach (var room in roomsList)
            {
                for (int column = offset; column < room.size.x; column++)
                {
                    for (int row = offset; row < room.size.y - offset; row++)
                    {
                        Vector2Int position = (Vector2Int)room.min + new Vector2Int(column, row);
                        floor.Add(position);
                    }
                }
            }

            return floor;
        }
    }
}
