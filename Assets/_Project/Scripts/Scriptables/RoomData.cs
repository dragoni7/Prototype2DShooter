using System.Collections.Generic;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "Room", menuName = "Level/RoomData")]
    public class RoomData : ScriptableObject
    {
        public List<SpawnerData> spawnerData;
    }
}
