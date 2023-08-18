using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "SimpleRandomWalkParameters_",menuName = "PCG/SimpeRandomWalkData")]
    public class SimpleRandomWalkData : ScriptableObject
    {
        public int iterations = 10, walklength = 10;
        public bool startRandomlyEachIteration = true;
    }
}
