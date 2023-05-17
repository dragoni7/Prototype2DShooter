using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    public class SteeringContextSolver : MonoBehaviour
    {
        [SerializeField] private bool showGizmos = true;

        // gizmos parameteres
        float[] interestGizmo = new float[0];
        Vector2 resultDirection = Vector2.zero;
        private float rayLength = 1;

        private void Start()
        {
            interestGizmo = new float[8];
        }

        public Vector2 GetDirectionToMove(List<AbstractSteeringBehaviour> behaviours, AIData aIData)
        {
            float[] danger = new float[8];
            float[] interest = new float[8];

            // loop through each behaviour
            foreach(AbstractSteeringBehaviour behaviour in behaviours)
            {
                (danger, interest) = behaviour.GetSteering(danger, interest, aIData);
            }

            // subtract danger values from interest array
            for (int i = 0; i < 8; i++)
            {
                interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
            }

            interestGizmo = interest;

            // get average direction
            Vector2 outputDirection = Vector2.zero;
            for (int i = 0; i < 8; i++)
            {
                outputDirection += Directions.eightDirections[i] * interest[i];
            }
            outputDirection.Normalize();

            resultDirection = outputDirection;

            // return selected movement direction
            return resultDirection;
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying && showGizmos)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(transform.position, resultDirection * rayLength);
            }
        }
    }
}
