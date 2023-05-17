using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractSteeringBehaviour : MonoBehaviour
    {
        public abstract (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aIData);
    }
}
