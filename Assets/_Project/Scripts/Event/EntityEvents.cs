using UnityEngine;

namespace dragoni7
{
    public struct EntityDamagedEvent : IEvent
    {
        public GameObject source;
        public Entity target;
        public IDamage damage;
        public Attributes attackerAttributes;
    }

    public struct EntityHealthChangedEvent : IEvent
    {
        public Entity entity;
    }

    public struct EntityMoveEvent : IEvent
    {
        public Entity entity;
        public Vector3 moveThisFrame;
    }
    public struct EntityAttackEvent : IEvent
    {
        public Entity entity;
    }

    public struct PlayerAttackEvent :IEvent
    {

    }

    public struct EnemySpawnedEvent : IEvent
    {
        public AbstractEnemy enemy;
    }

    public struct PlayerSpawnEvent : IEvent
    {
        public AbstractPlayer player;
    }

    public struct EntityDeathEvent : IEvent
    {
        public Entity entity;
    }
}
