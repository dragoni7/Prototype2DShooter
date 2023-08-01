
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Util;

namespace dragoni7
{
    public class Level
    {
        private Room _spawnRoom;
        private List<Room> _rooms;
        private List<Room> _mainRooms;
        private HashSet<Vector2Int> _corridors;
        private LevelData _levelData;

        public Room SpawnRoom => _spawnRoom;
        public List<Room> Rooms => _rooms;
        public LevelData LevelData => _levelData;
        public Level(List<IBounds> rooms, List<IBounds> mainRooms, HashSet<Vector2Int> corridors, LevelData levelData)
        {
            _rooms = new();
            _mainRooms = new();

            _levelData = levelData;
            int index;

            foreach (IBounds room in rooms)
            {
                index = Random.Range(0, _levelData.enemyRoomData.Count);
                _rooms.Add(new Room(room, Room.Type.EnemyRoom, _levelData.enemyRoomData[index]));
            }
            foreach (IBounds room in mainRooms)
            {
                index = Random.Range(0, _levelData.enemyRoomData.Count);
                _mainRooms.Add(new Room(room, Room.Type.EnemyRoom, _levelData.enemyRoomData[index]));
            }

            _corridors = corridors;

            index = Random.Range(0, _levelData.spawnRoomData.Count);
            _spawnRoom = new Room(mainRooms.Last(), Room.Type.PlayerSpawn, _levelData.spawnRoomData[index]);
            _mainRooms.Remove(_spawnRoom);
        }
    }
}
