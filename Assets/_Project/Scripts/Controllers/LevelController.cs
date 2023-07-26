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

        public void StartSpawners()
        {
            InvokeRepeating(nameof(UpdateSpawners), 0, 1.0f);
        }
        public void CreateLevel(string levelData)
        {
            generator.GenerateLevel(ResourceSystem.Instance.GetLevelData(levelData));
        }

        private void GetGeneratedLevel(Level level)
        {
            _level = level;
            GameController.Instance.ChangeState(GameController.GameState.SpawningPlayers);
        }

        private void UpdateSpawners()
        { 
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
