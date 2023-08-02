using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "GeneratorParameters_", menuName = "Level/GeneratorData")]
    public class GenerationData : ScriptableObject
    {
        [Header("Room Settings")]
        public int rooms = 100;
        public int radius = 70;
        public int roomMeanX = 6;
        public int roomStdDevX = 6;
        public int roomMinX = 5;
        public int roomMaxX = 40;
        public int roomMeanY = 6;
        public int roomStdDevY = 6;
        public int roomMinY = 5;
        public int roomMaxY = 40;
        [Range(0f, 10f)]
        public int roomBuffer = 2;

        [Header("Corridor Settings")]
        public int corridorSize = 4;
        public int extraLoops = 1;

        [Range(0.01f, 1.0f)]
        public float mainRoomPercent = 0.3f;

        [Header("Physics Sim Settings")]
        [Range(1f, 100f)]
        public float timeScale = 50f;
    }
}
