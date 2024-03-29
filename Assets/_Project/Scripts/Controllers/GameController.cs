using System;
using Util;

namespace dragoni7
{

    /// <summary>
    /// Controller class for manipulating game state
    /// </summary>
    public class GameController : Singleton<GameController>
    {
        public static event Action<GameState> OnBeforeStateChanged;
        public static event Action<GameState> OnAfterStateChanged;
        public GameState CurrentState { get; private set; }
        private void Start()
        {
            ChangeState(GameState.Starting);
        }
        
        // TODO: switch from using actions to event bus
        public void ChangeState(GameState newState)
        {
            OnBeforeStateChanged?.Invoke(newState);

            CurrentState = newState;

            switch (newState)
            {
                case GameState.Starting:
                    HandleStarting();
                    break;
                case GameState.GeneratingLevel:
                    HandleGeneratingLevel();
                    break;
                case GameState.SpawningPlayers:
                    HandleSpawningPlayers();
                    break;
                case GameState.PlayingLevel:
                    HandlePlayingLevel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnAfterStateChanged?.Invoke(newState);
        }

        private void HandleStarting()
        {
            ChangeState(GameState.GeneratingLevel);
        }

        private void HandleGeneratingLevel()
        {
            LevelController.Instance.CreateLevel("GenericLevel");
        }

        private void HandleSpawningPlayers()
        {
            PlayerController.Instance.SpawnPlayer("Player1", LevelController.Instance.PlayerSpawnPoint());
            ChangeState(GameState.PlayingLevel);
        }

        private void HandlePlayingLevel()
        {
            LevelController.Instance.StartSpawners();
        }

        [Serializable]
        public enum GameState
        {
            Starting = 0,
            GeneratingLevel = 1,
            SpawningPlayers = 2,
            PlayingLevel = 3
            // TODO: add more as needed
        }
    }
}

