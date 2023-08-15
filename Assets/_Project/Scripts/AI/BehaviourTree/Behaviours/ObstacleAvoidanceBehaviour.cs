using UnityEngine;
using Util;

namespace dragoni7
{
    public class ObstacleAvoidanceBehaviour : AbstractSteeringBehaviour
    {
        [SerializeField] private float radius = 5f, subjectColliderSize = 1.5f;
        [SerializeField] private bool showGizmo = true;

        // gizmo parameter
        float[] dangersResultTemp = null;

        public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
        {
            foreach(Collider2D obstacleCollider in aiData.obstacles)
            {
                if (obstacleCollider != null)
                {
                    Vector2 directionToObstacle = obstacleCollider.ClosestPoint(aiData.transform.position) - (Vector2)aiData.transform.position;
                    float distanceToObstacle = directionToObstacle.magnitude;

                    // calculate weight based on distance between subject and obstacle
                    float weight = distanceToObstacle <= subjectColliderSize ? 1 : (radius - distanceToObstacle) / radius;

                    Vector2 directionToObstacleNormalize = directionToObstacle.normalized;

                    // add obstacle parameters to the danger array
                    for (int i = 0; i < DirectionHelper.amount; i++)
                    {
                        float result = Vector2.Dot(directionToObstacleNormalize, DirectionHelper.directions16[i]);
                        //weight = 1.0f - Mathf.Abs(result - 0.65f);
                        float valueToPutIn = result * weight;

                        // override value only if it is higher than the current one stored in the array
                        if (valueToPutIn > danger[i])
                        {
                            danger[i] = valueToPutIn;
                        }
                    }
                }
            }

            dangersResultTemp = danger;
            DrawDebug(aiData);
            return (danger, interest);
        }

        private void DrawDebug(AIData aiData)
        {
            if (showGizmo == false)
            {
                return;
            }

            if (Application.isPlaying && dangersResultTemp != null)
            {

                for (int i = 0; i < dangersResultTemp.Length; i++)
                {
                    Debug.DrawRay(aiData.transform.position, DirectionHelper.directions16[i] * dangersResultTemp[i], Color.red);
                }
            }
        }
    }
}
