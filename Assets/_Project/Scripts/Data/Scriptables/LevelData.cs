using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
