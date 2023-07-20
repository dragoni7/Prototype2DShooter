using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class ShapeHelper
    {
        public static Vector2Int GetRandomPointInCircle(int radius)
        {
            return GetRandomPointInEllipse(radius, radius);
        }

        public static Vector2Int GetRandomPointInEllipse(int width, int height)
        {
            float t = 2 * Mathf.PI * Random.value;
            float u = Random.value + Random.value;
            float r;

            if (u > 1)
            {
                r = 2 - u;
            }
            else
            {
                r = u;
            }

            return new Vector2Int(Mathf.RoundToInt(width * r * Mathf.Cos(t)), Mathf.RoundToInt(height * r * Mathf.Sin(t)));
        }
    }
}
