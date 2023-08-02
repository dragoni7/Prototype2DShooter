using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class DirectionHelper
    {
        public static int amount = 16;
        public static List<Vector2> directions16 = new() 
        {

            new Vector2(0,-1).normalized,
            new Vector2(-1,-1).normalized,
            new Vector2(-1,-0.5f).normalized,
            new Vector2(-0.5f,1).normalized,
            new Vector2(-1,0).normalized,
            new Vector2(-1,0.5f).normalized,
            new Vector2(-0.5f,-1f).normalized,
            new Vector2(-1,1).normalized,

            new Vector2(0,1).normalized,
            new Vector2(1,-1).normalized,
            new Vector2(1,-0.5f).normalized,
            new Vector2(0.5f,-1).normalized,
            new Vector2(1,0).normalized,
            new Vector2(1,0.5f).normalized,
            new Vector2(0.5f,1f).normalized,
            new Vector2(1,1).normalized,
        };

        public static List<Vector2Int> cardinalDirections = new()
        {
            Vector2Int.up, // UP
            Vector2Int.right, // RIGHT
            Vector2Int.down, // DOWN
            Vector2Int.left // LEFT
        };

        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirections[Random.Range(0, cardinalDirections.Count)];
        }
    }
}
