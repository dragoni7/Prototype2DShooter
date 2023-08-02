using dragoni7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Util
{
    public class RectangleBounds : IBounds
    {
        private Vector2Int _center, _min, _max, _scale;

        private List<Vector2Int> _positions, _corners, _edges;

        public RectangleBounds(Vector2 center, Vector2 scale) : this(Vector2Int.RoundToInt(center), Vector2Int.RoundToInt(scale))
        {

        }

        public RectangleBounds(Vector2Int center, Vector2Int scale)
        {
            _center = center;
            _scale = scale;
            _positions = new();
            _edges = new();
            _corners = new();
            _min = center - (_scale / 2);
            _max = center + (_scale / 2);

            _corners.Add(_min);
            _corners.Add(new Vector2Int(_min.x, _max.y));
            _corners.Add(_max);
            _corners.Add(new Vector2Int(_max.x, _min.y));

            for (int column = 0; column < Scale().x; column++)
            {
                for (int row = 0; row < Scale().y; row++)
                {
                    _positions.Add(_min + new Vector2Int(column, row));
                }
            }

            _edges.AddRange(GetEdgesInDirection(_min, Vector2Int.right, _scale.x));
            _edges.AddRange(GetEdgesInDirection(_min, Vector2Int.up, _scale.y));
            _edges.AddRange(GetEdgesInDirection(_max, Vector2Int.left, _scale.x));
            _edges.AddRange(GetEdgesInDirection(_max, Vector2Int.down, _scale.y));
        }
        private List<Vector2Int> GetEdgesInDirection(Vector2Int position, Vector2Int direction, int end)
        {
            List<Vector2Int> edges = new();

            for (int i = 0; i < end; i++)
            {
                edges.Add(position + (direction * i));
            }

            return edges;
        }
        public int Area() => _scale.x * _scale.y;
        public Vector2Int Center() => _center;
        public List<Vector2Int> Corners() => _corners;
        public List<Vector2Int> EdgePositions() => _edges;
        public Vector2Int Max() => _max;
        public Vector2Int Min() => _min;
        public List<Vector2Int> PositionsWithin() => _positions;
        public Vector2Int Scale() => _scale;
    }
}
