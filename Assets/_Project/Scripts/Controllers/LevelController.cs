using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using Utils;

namespace dragoni7
{
    public class LevelController : Singletone<LevelController>
    {
        private Level _level;
        public Level Level => _level;

        [SerializeField]
        private LevelGenerator generator;

        protected override void Awake()
        {
            base.Awake();
            generator.OnGenerationFinished += GetGeneratedLevel;
        }

        public Vector2 PlayerSpawnPoint() => _level.SpawnRoom.Bounds.center;

        public void FillRooms()
        {
            InvokeRepeating(nameof(UpdateSpawners), 0, 1.0f);
        }
        public void CreateLevel(string generationData, string levelData)
        {
            ResourceSystem resourceSystem = ResourceSystem.Instance;
            generator.GenerateLevel(resourceSystem.GetGenerationData(generationData), resourceSystem.GetLevelData(levelData));
        }

        private void GetGeneratedLevel(Level level)
        {
            _level = level;
            GameController.Instance.ChangeState(GameController.GameState.SpawningEnemies);
        }

        private void UpdateSpawners()
        {
            print(_level.Rooms.Count);

            if (GameController.Instance.CurrentState == GameController.GameState.PlayingLevel)
            {
                foreach (Room room in _level.Rooms)
                {
                    foreach (Spawner spawner in room.Spawners)
                    {
                        if (PlayerController.Instance.IsNearPlayer(spawner.Position))
                        {
                            spawner.TrySpawn();
                        }
                    }
                }
            }
        }
    }
}
