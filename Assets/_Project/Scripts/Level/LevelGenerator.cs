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
        private Graph<Room> levelGraph = new();
        private HashSet<Vector2Int> floor = new();
        private bool simFinished = false;
        private bool generating;
        private GenerationData _generationParams;
        private LevelData _levelData;

        private List<Vertex<Room>> farthest = new();
        public void GenerateLevel(LevelData levelData)
        {
            _levelData = levelData;
            _generationParams = _levelData.generationData;

            ClearGeneration();

            Time.timeScale = _generationParams.timeScale;
            generating = true;

            for (int i = 0; i < _generationParams.rooms; i++)
            {
                BoundsInt roomBounds = new((Vector3Int)ShapeHelper.GetRandomPointInCircle(_generationParams.radius), 
                    new Vector3Int(
                    Mathf.RoundToInt(MathHelper.NextGaussian(_generationParams.roomMeanX, _generationParams.roomStdDevX, _generationParams.roomMinX, _generationParams.roomMaxX)),
                    Mathf.RoundToInt(MathHelper.NextGaussian(_generationParams.roomMeanY, _generationParams.roomStdDevY, _generationParams.roomMinY, _generationParams.roomMaxY)),
                    0));

                // create new temp game object for each room
                GameObject obj = new();
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
                // continue simulating until all room objects are sleeping
                if (!simFinished)
                {
                    simFinished = tempObjects.All(o => o.GetComponent<Rigidbody2D>().IsSleeping());
                }
                // create a room where each temp object is located
                else
                {
                    List<BoundsInt> rooms = new();

                    foreach (var room in tempObjects)
                    {
                        rooms.Add(new BoundsInt(Vector3Int.RoundToInt(room.transform.position), Vector3Int.RoundToInt(room.transform.localScale)));
                    }

                    // determine main rooms for gameplay purposes
                    SortRoomsByArea(rooms);

                    List<BoundsInt> mainRooms = TakeTopPercentOf(rooms, _generationParams.mainRoomPercent);
                    List<BoundsInt> nonMainRooms = rooms.Except(mainRooms).ToList();

                    List<Vector2Int> mainRoomCenters = new();
                    // collect main room center points
                    foreach (var room in mainRooms)
                    {
                        mainRoomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
                    }

                    // use delaunator to determine non overlapping path between each main room
                    IPoint[] points = DelaunatorExtensions.ToPoints(mainRoomCenters);
                    Delaunator delaunator = new(points);

                    for (int e = 0; e < delaunator.Triangles.Length; e++)
                    {
                        if (e > delaunator.Halfedges[e])
                        {
                            var p = points[delaunator.Triangles[e]];
                            var q = points[delaunator.Triangles[Delaunator.NextHalfedge(e)]];
                            levelGraph.AddEdge(Vector2Int.RoundToInt(p.ToVector2()), Vector2Int.RoundToInt(q.ToVector2()), Random.Range(1, 2));
                        }
                    }

                    // create MST from delaunay triangle to determine level layout
                    Graph<Room> mst = Graph<Room>.MST(levelGraph);
                    // add additional loops
                    for (int i = 0; i < _generationParams.extraLoops; i++)
                    {
                        mst.AddEdge(levelGraph.Edges.Find(e => !mst.Edges.Contains(e)));
                    }
                    
                    levelGraph = mst;

                    generating = false;
                    Time.timeScale = 1f;
                    ClearTempObjects();

                    floor = CreateSimpleRooms(mainRooms);
                    HashSet<Vector2Int> corridors = ConnectRooms(levelGraph);

                    List<BoundsInt> finalNonMainRooms = new();
                    bool added;
                    foreach (BoundsInt room in nonMainRooms)
                    {
                        added = false;
                        for (int column = 0; column < room.size.x; column++)
                        {
                            if (added)
                            {
                                break;
                            }

                            for (int row = 0; row < room.size.y; row++)
                            {
                                Vector2Int position = (Vector2Int)room.min + new Vector2Int(column, row);
                                
                                if (corridors.Contains(position))
                                {
                                    AddRoom(floor, room);
                                    finalNonMainRooms.Add(room);
                                    added = true;
                                    break;
                                }
                            }
                        }
                    }

                    Level level = new(finalNonMainRooms, mainRooms, corridors, _levelData);
                    farthest = levelGraph.Leaves;

                    farthest.Sort(delegate (Vertex<Room> v1, Vertex<Room> v2) {
                        float distance_v1 = Vector2Int.Distance(Vector2Int.RoundToInt(level.SpawnRoom.Bounds.center), v1.Position);
                        float distance_v2 = Vector2Int.Distance(Vector2Int.RoundToInt(level.SpawnRoom.Bounds.center), v2.Position);

                        return distance_v2.CompareTo(distance_v1);
                    });

                    OnGenerationFinished?.Invoke(level);

                    // paint tiles
                    floor.UnionWith(corridors);
                    WallGenerator.CreateWalls(floor, corridors, tilemapVisualizer);
                    tilemapVisualizer.PaintFloorTiles(floor);
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
        private HashSet<Vector2Int> ConnectRooms(Graph<Room> graph)
        {
            HashSet<Vector2Int> corridors = new();

            foreach (var edge in graph.Edges)
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
                AddCorridorWidth(corridor, position, width, false);
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
                AddCorridorWidth(corridor, position, width, true);
            }

            return corridor;
        }
        private void AddCorridorWidth(HashSet<Vector2Int> corridor, Vector2Int position, int width, bool vertical)
        {
            Vector2Int direction;

            for (int i = 1; i <= width; i++)
            {
                if (vertical)
                {
                    if (i % 2 == 0)
                    {
                        direction = Vector2Int.up;
                    }
                    else
                    {
                        direction = Vector2Int.down;
                    }
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        direction = Vector2Int.right;
                    }
                    else
                    {
                        direction = Vector2Int.left;
                    }
                }

                corridor.Add(position + (direction * (Mathf.CeilToInt(i / 2))));
            }
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
        private void ClearGeneration()
        {
            tilemapVisualizer.Clear();
            tempObjects.Clear();
            levelGraph.Clear();
            floor.Clear();
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
                foreach (var edge in levelGraph.Edges)
                {
                    Gizmos.color = Color.magenta;
                    Gizmos.DrawLine(new Vector2(edge.From.Position.x, edge.From.Position.y), new Vector2(edge.To.Position.x, edge.To.Position.y));
                }

                if (farthest != null)
                {
                    Gizmos.color = Color.red;
                    foreach (var vertex in farthest)
                    {
                        Gizmos.DrawSphere(new Vector3(vertex.Position.x, vertex.Position.y, 0), 2);
                    }
                }
            }
        }
    }
}

