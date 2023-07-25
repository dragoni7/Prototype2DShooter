
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace dragoni7
{
    public class Level
    {
        private BoundsInt _spawnRoom;
        private List<BoundsInt> _rooms;
        private List<BoundsInt> _mainRooms;
        private HashSet<Vector2Int> _corridors;

        public BoundsInt SpawnRoom => _spawnRoom;
        public List<BoundsInt> Rooms => _rooms;

        public Level(List<BoundsInt> rooms, List<BoundsInt> mainRooms, HashSet<Vector2Int> corridors)
        {
            _rooms = rooms;
            _mainRooms = mainRooms;
            _corridors = corridors;

            _spawnRoom = _mainRooms.Last();
            _mainRooms.Remove(_spawnRoom);
        }
    }
}
