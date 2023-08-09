using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level/LevelData")]
    public class LevelData : ScriptableObject
    {
        public List<RoomData> spawnRoomData;
        public List<RoomData> enemyRoomData;

        public GenerationData generationData;
    }
}
