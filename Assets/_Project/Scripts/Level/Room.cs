using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dragoni7
{
    public class Room
    {
        private BoundsInt _bounds;
        private Type _type;
        public BoundsInt Bounds => _bounds;
        public Type RoomType => _type;

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
