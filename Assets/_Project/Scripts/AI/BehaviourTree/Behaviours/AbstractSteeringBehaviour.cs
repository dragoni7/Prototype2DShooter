using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractSteeringBehaviour
    {
        public abstract (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aIData);
    }
}
