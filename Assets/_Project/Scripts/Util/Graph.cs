using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace Utils
{
    public class Edge<T>
    {
        public Vertex<T> From { get; set; }
        public Vertex<T> To { get; set; }
        public int Weight { get; set; }
        public Edge(Vertex<T> from, Vertex<T> to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }

    public class Vertex<T>
    {
        public int ID { get; set;}
        public Vector2Int Position { get; set; }
        public int Degree { get; set; }  
        public T Data { get; set; }
        public Vertex(int id, Vector2Int value)
        {
            ID = id;
            Position = value;
            Degree = 0;
        }
    }

    public class Graph<T>
    {
        private List<Edge<T>> _edges;

        private List<Vertex<T>> _vertices;
        public List<Edge<T>> Edges => _edges;
        public List<Vertex<T>> Vertices => _vertices;
        public List<Vertex<T>> Leaves => Vertices.FindAll(v => v.Degree == 1);

        private int _verticesCount;
        public Graph()
        {
            _edges = new();
            _vertices = new();
            _verticesCount = 0;
        }

        public void AddEdge(Edge<T> edge)
        {
            TryAddVertex(edge.From.Position, edge.To.Position);

            _edges.Add(edge);
        }

        public void AddEdge(Vector2Int from, Vector2Int to, int weight)
        {
            (Vertex<T> v1, Vertex<T> v2) = TryAddVertex(from, to);

            _edges.Add(new Edge<T>(v1, v2, weight));
        }

        private (Vertex<T>, Vertex<T>) TryAddVertex(Vector2Int from, Vector2Int to)
        {
            Vertex<T> v1 = _vertices.Find(v => v.Position == from);

            if (v1 == null)
            {
                v1 = new Vertex<T>(_verticesCount, from);
                _vertices.Add(v1);
                _verticesCount++;
            }

            v1.Degree++;

            Vertex<T> v2 = _vertices.Find(v => v.Position == to);

            if (v2 == null)
            {
                v2 = new Vertex<T>(_verticesCount, to);
                _vertices.Add(v2);
                _verticesCount++;
            }

            v2.Degree++;

            return (v1, v2);
        }

        public void Clear()
        {
            _vertices.Clear();
            _edges.Clear();
            Leaves.Clear();
        }
        public static Graph<T> MST(Graph<T> graph)
        {
            Graph<T> mst = new();

            DisjointSet set = new(100);

            foreach (Vertex<T> vertex in graph.Vertices)
            {
                set.MakeSet(vertex.ID);
            }

            // sort edges order by weight ascending
            var sortedEdge = graph.Edges.OrderBy(x => x.Weight).ToList();

            foreach (Edge<T> edge in sortedEdge)
            {
                // adding edge to result if both vertices do not belong to same set
                // both vertices in same set means it can have cycles in the tree
                if (set.FindSet(edge.From.ID) != set.FindSet(edge.To.ID))
                {
                    mst.AddEdge(edge);
                    set.Union(edge.From.ID, edge.To.ID);
                }
            }

            return mst;
        }
    }
}
