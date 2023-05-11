using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class BasePattern : MonoBehaviour
    {
        public List<Vector2> points = new();
        public Vector2 origin;

        public void Awake()
        {
            origin = Vector2.zero;
        }

        public void OnDrawGizmos()
        {
            origin = transform.position;

            Gizmos.color = Color.blue;
            Gizmos.DrawCube(origin, new Vector3(0.2f, 0.2f, 0));

            Gizmos.color = Color.red;
            foreach(Vector2 point in points)
            {
                Gizmos.DrawCube(origin + point, new Vector3(0.2f, 0.2f, 0));
            }
        }
    }
}
