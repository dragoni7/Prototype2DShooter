using System.Linq;
using UnityEngine;
using Util;

namespace dragoni7
{
    public class SeekBehaviour : AbstractSteeringBehaviour
    {
        [SerializeField] private float targetReachedThreshold = 0.5f;
        [SerializeField] private bool showGizmos = true;

        bool reachedLastTarget = true;

        // gizmo parameters
        private Vector2 targetPositionCached;
        private float[] interestsTemp;

        public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
        {
            // if no target, stop seeking
            // else set a new target
            if (reachedLastTarget)
            {
                if (aiData.targets == null || aiData.targets.Count <= 0)
                {
                    aiData.currentTarget = null;
                    return (danger, interest);
                }
                else
                {
                    reachedLastTarget = false;
                    // get closests target
                    aiData.currentTarget = aiData.targets.OrderBy(target => Vector2.Distance(target.position, aiData.transform.position)).FirstOrDefault();
                }
            }

            // cache the last position only if we still see the target
            if (aiData.currentTarget != null && aiData.targets != null && aiData.targets.Contains(aiData.currentTarget))
            {
                targetPositionCached = aiData.currentTarget.position;
            }

            // first check if we have reached the target
            if (Vector2.Distance(aiData.transform.position, targetPositionCached) < targetReachedThreshold)
            {
                reachedLastTarget = true;
                aiData.currentTarget = null;
                return (danger, interest);
            }

            // if we havent yet reached the target do the main logic of finding the interest directions
            Vector2 directionToTarget = (targetPositionCached - (Vector2)aiData.transform.position);
            for (int i = 0; i < interest.Length; i++)
            {
                float result = Vector2.Dot(directionToTarget.normalized, DirectionHelper.directions16[i]);
                // accept only directions at less than 90 degrees to the target direction
                if (result > -1)
                {
                    float valueToPutIn = result;
                    if (valueToPutIn > interest[i])
                    {
                        interest[i] = valueToPutIn;
                    }
                }
            }

            interestsTemp = interest;
            DrawDebug(aiData);
            return (danger, interest);
        }

        private void DrawDebug(AIData aiData)
        {
            if (showGizmos == false)
            {
                return;
            }

            if (Application.isPlaying && interestsTemp != null)
            {
                for (int i = 0; i < interestsTemp.Length; i++)
                {
                    Debug.DrawRay(aiData.transform.position, DirectionHelper.directions16[i] * interestsTemp[i], Color.green);
                }
            }
        }
    }
}
