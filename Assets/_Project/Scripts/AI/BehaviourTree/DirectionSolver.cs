using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public class DirectionSolver
    {
        private Vector2 _resultDirection = Vector2.zero;
        private List<AbstractSteeringBehaviour> _behaviours;

        public DirectionSolver(List<AbstractSteeringBehaviour> behaviours)
        {
            _behaviours = behaviours;
        }
        public Vector2 GetDirectionToMove(AIData aiData)
        {
            float[] danger = new float[DirectionHelper.amount];
            float[] interest = new float[DirectionHelper.amount];

            // loop through each behaviour
            foreach (AbstractSteeringBehaviour behaviour in _behaviours)
            {
                (danger, interest) = behaviour.GetSteering(danger, interest, aiData);
            }

            // subtract danger values from interest array
            for (int i = 0; i < DirectionHelper.amount; i++)
            {
                interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
            }

            // get average direction
            Vector2 outputDirection = Vector2.zero;
            for (int i = 0; i < DirectionHelper.amount; i++)
            {
                outputDirection += DirectionHelper.directions16[i] * interest[i];
            }
            outputDirection.Normalize();

            _resultDirection = outputDirection;
            Debug.DrawRay(aiData.transform.position, _resultDirection, Color.blue);
            // return selected movement direction
            return _resultDirection;
        }
    }
}
