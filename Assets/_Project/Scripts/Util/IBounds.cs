using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public interface IBounds
    {
        public Vector2Int Center();
        public Vector2Int Scale();
        public int Area();
        public Vector2Int Min();
        public Vector2Int Max();
        public List<Vector2Int> Corners();
        public List<Vector2Int> PositionsWithin();
        public List<Vector2Int> EdgePositions();
    }
}
