using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dragoni7
{
    public static class Directions
    {
        public static List<Vector2> eightDirections = new() 
        { 
            new Vector2(0,1).normalized,
            new Vector2(1,0).normalized,
            new Vector2(1,1).normalized,
            new Vector2(1,-1).normalized,
            new Vector2(0,-1).normalized,
            new Vector2(-1,-1).normalized,
            new Vector2(-1,0).normalized,
            new Vector2(-1,1).normalized,
        };
    }
}
