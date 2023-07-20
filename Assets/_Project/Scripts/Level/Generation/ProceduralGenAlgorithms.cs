using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public static class ProceduralGenAlgorithms
    {
        public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
        {
            HashSet<Vector2Int> path = new();
            path.Add(startPosition);
            var previousPos = startPosition;

            for (int i = 0; i < walkLength; i++)
            {
                var newPos = previousPos + DirectionHelper.GetRandomCardinalDirection();
                path.Add(newPos);
                previousPos = newPos;
            }

            return path;
        }


        public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
        {
            List<Vector2Int> corridor = new();
            var direction = DirectionHelper.GetRandomCardinalDirection();
            var currentPosition = startPosition;
            corridor.Add(currentPosition);

            for (int i = 0; i < corridorLength; i++)
            {
                currentPosition += direction;
                corridor.Add(currentPosition);
            }

            return corridor;
        }


        public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
        {
            Queue<BoundsInt> roomsQueue = new();
            List<BoundsInt> roomsList = new();

            roomsQueue.Enqueue(spaceToSplit);

            while(roomsQueue.Count > 0)
            {
                var room = roomsQueue.Dequeue();
                if (room.size.y >= minHeight && room.size.x >= minHeight)
                {
                    if (Random.value < 0.5f)
                    {
                        // split horizontal
                        if (room.size.y >= minHeight * 2)
                        {
                            SplitHorizontally(minWidth, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth * 2)
                        {
                            SplitVertically(minHeight, roomsQueue, room);
                        }
                        else
                        {
                            roomsList.Add(room);
                        }
                    }
                    else
                    {
                        // split vertical
                        if (room.size.x >= minWidth * 2)
                        {
                            SplitVertically(minHeight, roomsQueue, room);
                        }
                        else if (room.size.y >= minHeight * 2)
                        {
                            SplitHorizontally(minWidth, roomsQueue, room);
                        }
                        else
                        {
                            roomsList.Add(room);
                        }
                    }
                }
            }

            return roomsList;
        }

        private static void SplitVertically(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            var xSplit = Random.Range(1, room.size.x); // minHeight, room.size.y - minHeight
            BoundsInt room1 = new(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
            BoundsInt room2 = new(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }

        private static void SplitHorizontally(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            var ySplit = Random.Range(1, room.size.y);
            BoundsInt room1 = new(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
            BoundsInt room2 = new(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }
    }
}
