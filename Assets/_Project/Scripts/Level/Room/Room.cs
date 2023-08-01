using System.Collections.Generic;
using UnityEngine;
using Util;

namespace dragoni7
{
    public class Room
    {
        private IBounds _bounds;
        private Type _type;
        private RoomData _roomData;
        private List<Spawner> _spawners;
        public IBounds Bounds => _bounds;
        public Type RoomType => _type;
        public RoomData RoomData => _roomData;
        public List<Spawner> Spawners => _spawners;

        public Room(IBounds bounds, Type type, RoomData roomData)
        {
            _roomData = roomData;
            _bounds = bounds;
            _type = type;

            _spawners = new();

            foreach (SpawnerData spawnData in _roomData.spawnerData)
            {
                _spawners.Add(new Spawner(_bounds.Center(), spawnData));
            }
        }

        public enum Type
        {
            PlayerSpawn = 0,
            EnemyRoom = 1,
            LootRoom = 2,
            BossRoom = 3,
            ExitRoom = 4
        }
    }
}
