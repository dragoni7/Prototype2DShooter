using UnityEngine;

namespace dragoni7
{
    public class ObstacleAvoidanceBehaviour : AbstractSteeringBehaviour
    {
        [SerializeField] private float radius = 2f, subjectColliderSize = 0.6f;
        [SerializeField] private bool showGizmo = true;

        // gizmo parameter
        float[] dangersResultTemp = null;

        public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aIData)
        {
            foreach(Collider2D obstacleCollider in aIData.obstacles)
            {
                Vector2 directionToObstacle = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
                float distanceToObstacle = directionToObstacle.magnitude;

                // calculate weight based on distance between subject and obstacle
                float weight = distanceToObstacle <= subjectColliderSize ? 1 : (radius - distanceToObstacle) / radius;

                Vector2 directionToObstacleNormalize = directionToObstacle.normalized;

                // add obstacle parameters to the danger array
                for (int i = 0; i < Directions.eightDirections.Count; i++)
                {
                    float result = Vector2.Dot(directionToObstacleNormalize, Directions.eightDirections[i]);

                    float valueToPutIn = result * weight;

                    // override value only if it is higher than the current one stored in the array
                    if (valueToPutIn > danger[i])
                    {
                        danger[i] = valueToPutIn;
                    }
                }
            }

            dangersResultTemp = danger;
            return (danger, interest);
        }

        private void OnDrawGizmos()
        {
            if (showGizmo == false)
            {
                return;
            }

            if (Application.isPlaying && dangersResultTemp != null)
            {
                Gizmos.color = Color.red;

                for (int i = 0; i < dangersResultTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * dangersResultTemp[i]);
                }
            }
            else
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(transform.position, radius);
            }
        }
    }
}
