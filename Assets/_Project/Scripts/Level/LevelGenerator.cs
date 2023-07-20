using DelaunatorSharp;
using DelaunatorSharp.Unity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace dragoni7
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        protected TilemapVisualizer tilemapVisualizer = null;

        public event Action<Level> OnGenerationFinished;

        private List<GameObject> tempObjects = new();
        List<BoundsInt> rooms = new();
        List<BoundsInt> mainRooms = new();
        List<Vector2Int> roomCenters = new();
        List<Vector2Int> mainRoomCenters = new();
        Graph<BoundsInt> levelGraph = new();
        HashSet<Vector2Int> floor = new();
        Delaunator delaunator;
        private bool simFinished = false;
        private bool generating;
        private GenerationData _generationParams;

        private List<Vertex<BoundsInt>> farthest = new();

        public void GenerateLevel(GenerationData generatorParams)
        {
            _generationParams = generatorParams;

            tilemapVisualizer.Clear();

            Time.timeScale = _generationParams.timeScale;
            generating = true;

            for (int i = 0; i < _generationParams.rooms; i++)
            {
                BoundsInt roomBounds = new((Vector3Int)ShapeHelper.GetRandomPointInCircle(_generationParams.radius), 
                    new Vector3Int(
                    Mathf.RoundToInt(MathHelper.NextGaussian(_generationParams.roomMeanX, _generationParams.roomStdDevX, _generationParams.roomMinX, _generationParams.roomMaxX)),
                    Mathf.RoundToInt(MathHelper.NextGaussian(_generationParams.roomMeanY, _generationParams.roomStdDevY, _generationParams.roomMinY, _generationParams.roomMaxY)),
                    0));

                var obj = new GameObject();
                obj.transform.position = roomBounds.center;
                obj.transform.localScale = roomBounds.size;
                obj.AddComponent<Rigidbody2D>();
                obj.AddComponent<BoxCollider2D>();
                Rigidbody2D objRigidBody = obj.GetComponent<Rigidbody2D>();
                BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
                collider.size += new Vector2(_generationParams.physicsColliderSize, _generationParams.physicsColliderSize);
                objRigidBody.freezeRotation = true;
                objRigidBody.gravityScale = 0.0f;
                tempObjects.Add(obj);
            }
        }

        private void Update()
        {
            if (generating)
            {
                if (!simFinished)
                {
                    foreach (var room in tempObjects)
                    {
                        // continue simulating until all room objects are sleeping
                        simFinished = room.GetComponent<Rigidbody2D>().IsSleeping();
                    }
                }
                else
                {
                    // create a room bounds where each room object is located
                    foreach (var room in tempObjects)
                    {
                        rooms.Add(new BoundsInt(Vector3Int.RoundToInt(room.transform.position), Vector3Int.RoundToInt(room.transform.localScale)));
                    }

                    // collect room center points
                    foreach (var room in rooms)
                    {
                        roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
                    }

                    SortRoomsByArea(rooms);
                    mainRooms = TakeTopPercentOf(rooms, _generationParams.mainRoomPercent);
                    List<BoundsInt> nonMainRooms = rooms.Except(mainRooms).ToList();

                    // collect main room center points
                    foreach (var room in mainRooms)
                    {
                        mainRoomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
                    }

                    delaunator = new(DelaunatorExtensions.ToPoints(mainRoomCenters));

                    IPoint[] points = DelaunatorExtensions.ToPoints(mainRoomCenters);

                    for (int e = 0; e < delaunator.Triangles.Length; e++)
                    {
                        if (e > delaunator.Halfedges[e])
                        {
                            var p = points[delaunator.Triangles[e]];
                            var q = points[delaunator.Triangles[Delaunator.NextHalfedge(e)]];
                            levelGraph.AddEdge(Vector2Int.RoundToInt(p.ToVector2()), Vector2Int.RoundToInt(q.ToVector2()), Random.Range(1, 2));
                        }
                    }

                    Graph<BoundsInt> mst = Graph<BoundsInt>.MST(levelGraph);
                    mst.AddEdge(levelGraph.Edges.Find(e => !mst.Edges.Contains(e)));
                    levelGraph = mst;

                    generating = false;
                    Time.timeScale = 1f;
                    ClearTempObjects();

                    floor = CreateSimpleRooms(mainRooms);
                    HashSet<Vector2Int> corridors = ConnectRooms(levelGraph);
                    floor.UnionWith(corridors);

                    List<BoundsInt> finalNonMainRooms = new();

                    foreach (BoundsInt room in nonMainRooms)
                    {
                        for (int column = 0; column < room.size.x; column++)
                        {
                            for (int row = 0; row < room.size.y; row++)
                            {
                                Vector2Int position = (Vector2Int)room.min + new Vector2Int(column, row);
                                
                                if (corridors.Contains(position))
                                {
                                    AddRoom(floor, room);
                                    finalNonMainRooms.Add(room);
                                    break;
                                }
                            }
                        }
                    }

                    Level level = new(finalNonMainRooms, mainRooms, corridors);

                    farthest = levelGraph.Leaves;

                    farthest.Sort(delegate (Vertex<BoundsInt> v1, Vertex<BoundsInt> v2) {
                        float distance_v1 = Vector2Int.Distance(Vector2Int.RoundToInt(level.SpawnRoom.center), v1.Position);
                        float distance_v2 = Vector2Int.Distance(Vector2Int.RoundToInt(level.SpawnRoom.center), v2.Position);

                        return distance_v2.CompareTo(distance_v1);
                    });
                    
                    OnGenerationFinished?.Invoke(level);

                    tilemapVisualizer.PaintFloorTiles(floor);
                    WallGenerator.CreateWalls(floor, tilemapVisualizer);
                }
            }
        }

        private void SortRoomsByArea(List<BoundsInt> rooms)
        {
            rooms.Sort(delegate(BoundsInt a, BoundsInt b) {
                int aArea = a.size.x * a.size.y;
                int bArea = b.size.x * b.size.y;

                return bArea.CompareTo(aArea);
            });
        }
        private List<BoundsInt> TakeTopPercentOf(List<BoundsInt> rooms, float percent)
        {
            return rooms.Take((int)(percent * rooms.Count())).ToList();
        }
        private HashSet<Vector2Int> ConnectRooms(Graph<BoundsInt> graph)
        {
            HashSet<Vector2Int> corridors = new();

            foreach (Edge<BoundsInt> edge in graph.Edges)
            {
                HashSet<Vector2Int> newCorridor = CreateCorridor(edge.From.Position, edge.To.Position, _generationParams.corridorSize);
                corridors.UnionWith(newCorridor);
            }

            return corridors;
        }
        private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination, int width)
        {
            HashSet<Vector2Int> corridor = new();
            var position = currentRoomCenter;
            corridor.Add(position);

            while (position.y != destination.y)
            {
                if (destination.y >= position.y)
                {
                    position += Vector2Int.up;
                }
                else if (destination.y <= position.y)
                {
                    position += Vector2Int.down;
                }

                corridor.Add(position);
                var extraWidth = position;

                for (int i = 1; i <= width; i++)
                {
                    corridor.Add((extraWidth + Vector2Int.right * i));
                    corridor.Add((extraWidth + Vector2Int.left * i));
                }
            }

            while (position.x != destination.x)
            {
                if (destination.x >= position.x)
                {
                    position += Vector2Int.right;
                }
                else if (destination.x <= position.x)
                {
                    position += Vector2Int.left;
                }

                corridor.Add(position);
                var extraWidth = position;

                for (int i = 1; i <= width; i++)
                {
                    corridor.Add((extraWidth + Vector2Int.up * i));
                    corridor.Add((extraWidth + Vector2Int.down * i));
                }
            }

            return corridor;
        }
        private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
        {
            HashSet<Vector2Int> floor = new();

            foreach (var room in roomsList)
            {
                AddRoom(floor, room);
            }

            return floor;
        }
        private void AddRoom(HashSet<Vector2Int> floor, BoundsInt room)
        {
            for (int column = 0; column < room.size.x; column++)
            {
                for (int row = 0; row < room.size.y; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(column, row);
                    floor.Add(position);
                }
            }
        }
        private void ClearTempObjects()
        {
            foreach (var room in tempObjects)
            {
                Destroy(room);
            }

            tempObjects.Clear();
        }
        public void OnDrawGizmos()
        {
            if (!simFinished)
            {
                foreach (var room in tempObjects)
                {
                    Gizmos.DrawCube(room.transform.position, room.transform.localScale);
                }
            }
            
            if (!generating)
            {
                foreach (Edge<BoundsInt> edge in levelGraph.Edges)
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(new Vector2(edge.From.Position.x, edge.From.Position.y), new Vector2(edge.To.Position.x, edge.To.Position.y));
                }

                if (farthest != null)
                {
                    Gizmos.color = Color.red;
                    foreach (Vertex<BoundsInt> vertex in farthest)
                    {
                        Gizmos.DrawSphere(new Vector3(vertex.Position.x, vertex.Position.y, 0), 2);
                    }
                }
            }
        }
    }
}

