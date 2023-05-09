using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static dragoni7.ScriptablePlayer;

namespace dragoni7
{
    public class GameController : Singletone<GameController>
    {
        public static event Action<GameState> OnBeforeStateChanged;
        public static event Action<GameState> OnAfterStateChanged;
        public GameState CurrentState { get; private set; }

        private void Start()
        {
            ChangeState(GameState.Starting);
        }
        public void ChangeState(GameState newState)
        {
            OnBeforeStateChanged?.Invoke(newState);

            CurrentState = newState;

            switch (newState)
            {
                case GameState.Starting:
                    HandleStarting();
                    break;
                case GameState.SpawningPlayers:
                    HandleSpawningPlayers();
                    break;
                case GameState.Win:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }

            OnAfterStateChanged?.Invoke(newState);
        }

        private void HandleStarting()
        {
            ChangeState(GameState.SpawningPlayers);
        }

        private void HandleSpawningPlayers()
        {
            PlayerController.Instance.SpawnPlayer("Player1", Vector2.zero);
        }

        [Serializable]
        public enum GameState
        {
            Starting = 0,
            SpawningPlayers = 1,
            Win = 2,
            // TODO: add more as needed
        }
    }
}

