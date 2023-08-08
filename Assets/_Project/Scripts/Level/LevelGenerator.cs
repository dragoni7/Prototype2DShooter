using DelaunatorSharp;
using DelaunatorSharp.Unity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Util;
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
                RectangleBounds roomBounds = new(ShapeHelper.GetRandomPointInCircle(_generationParams.radius), new Vector2Int(
                    Mathf.RoundToInt(MathHelper.NextGaussian(_generationParams.roomMeanX, _generationParams.roomStdDevX, _generationParams.roomMinX, _generationParams.roomMaxX)),
                    Mathf.RoundToInt(MathHelper.NextGaussian(_generationParams.roomMeanY, _generationParams.roomStdDevY, _generationParams.roomMinY, _generationParams.roomMaxY))));

                // create new temp game object for each room
                CreateSimulationObject(roomBounds);
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
                    List<IBounds> rooms = new();

                    foreach (var room in tempObjects)
                    {
                        rooms.Add(new RectangleBounds(room.transform.position, room.transform.localScale));
                    }

                    // determine main rooms for gameplay purposes
                    SortRoomsByArea(rooms);

                    List<IBounds> mainRooms = TakeTopPercentOf(rooms, _generationParams.mainRoomPercent);
                    List<IBounds> nonMainRooms = rooms.Except(mainRooms).ToList();

                    List<Vector2Int> mainRoomCenters = new();
                    // collect main room center points
                    foreach (IBounds room in mainRooms)
                    {
                        mainRoomCenters.Add(room.Center());
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

                    // determine floor locations
                    floor = CreateSimpleRooms(mainRooms);
                    // connect rooms with corridors
                    HashSet<Vector2Int> corridors = ConnectRooms(levelGraph);

                    // add back any non main room that intersects corridors
                    List<IBounds> finalNonMainRooms = new();
                    bool added;
                    foreach (IBounds room in nonMainRooms)
                    {
                        added = false;
                        for (int column = 0; column < room.Scale().x; column++)
                        {
                            if (added)
                            {
                                break;
                            }

                            for (int row = 0; row < room.Scale().y; row++)
                            {
                                Vector2Int position = room.Min() + new Vector2Int(column, row);
                                
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

                    // gather data into level object
                    // TODO: determine farthest main rooms for gameplay purposes and store in level
                    Level level = new(finalNonMainRooms, mainRooms, corridors, _levelData);
                    farthest = levelGraph.Leaves;

                    farthest.Sort(delegate (Vertex<Room> v1, Vertex<Room> v2) {
                        float distance_v1 = Vector2Int.Distance(Vector2Int.RoundToInt(level.SpawnRoom.Bounds.Center()), v1.Position);
                        float distance_v2 = Vector2Int.Distance(Vector2Int.RoundToInt(level.SpawnRoom.Bounds.Center()), v2.Position);

                        return distance_v2.CompareTo(distance_v1);
                    });

                    OnGenerationFinished?.Invoke(level);

                    // corridor walls
                    WallGenerator.CreateCorridorWalls(corridors, floor, tilemapVisualizer);
                    // room walls
                    WallGenerator.CreateRoomWalls(finalNonMainRooms, corridors, tilemapVisualizer, _generationParams.corridorSize, _generationParams.openRoomChance);
                    // main rooms always have doors
                    WallGenerator.CreateRoomWalls(mainRooms, corridors, tilemapVisualizer, _generationParams.corridorSize, 1.0f);
                    floor.UnionWith(corridors);
                    tilemapVisualizer.PaintFloorTiles(floor);
                }
            }
        }
        private void CreateSimulationObject(IBounds room)
        {
            GameObject obj = new();
            obj.name = "simulated_room";
            obj.layer = LayerMask.NameToLayer("Room");
            obj.transform.position = new Vector2(room.Center().x, room.Center().y);
            obj.transform.localScale = new Vector2(room.Scale().x, room.Scale().y);
            obj.AddComponent<Rigidbody2D>();
            obj.AddComponent<PolygonCollider2D>();
            Rigidbody2D objRigidBody = obj.GetComponent<Rigidbody2D>();
            PolygonCollider2D collider = obj.GetComponent<PolygonCollider2D>();
            collider.points = room.Corners().Select(c => (Vector2)(room.Center() - c) / (Vector2)(room.Scale() - (Vector2Int.one * _generationParams.roomBuffer))).ToArray();
            objRigidBody.freezeRotation = true;
            objRigidBody.gravityScale = 0.0f;
            obj.transform.parent = this.transform;
            tempObjects.Add(obj);
        }
        private void SortRoomsByArea(List<IBounds> rooms)
        {
            rooms.Sort(delegate(IBounds a, IBounds b) {
                return b.Area().CompareTo(a.Area());
            });
        }
        private List<IBounds> TakeTopPercentOf(List<IBounds> rooms, float percent)
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
        private HashSet<Vector2Int> CreateSimpleRooms(List<IBounds> roomsList)
        {
            HashSet<Vector2Int> floor = new();

            foreach (IBounds room in roomsList)
            {
                AddRoom(floor, room);
            }

            return floor;
        }
        private void AddRoom(HashSet<Vector2Int> floor, IBounds room)
        {
            floor.AddRange(room.PositionsWithin());
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
            ClearTempObjects();
            levelGraph.Clear();
            floor.Clear();
    }
        public void OnDrawGizmos()
        {
            if (generating)
            {
                foreach (var room in tempObjects)
                {
                    RectangleBounds x = new RectangleBounds(room.transform.position, room.transform.localScale);
                    Gizmos.DrawCube(new Vector2(x.Center().x, x.Center().y), new Vector2(x.Scale().x, x.Scale().y));
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

