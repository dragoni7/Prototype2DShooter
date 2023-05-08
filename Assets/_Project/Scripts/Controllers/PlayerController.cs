using UnityEngine;
using static dragoni7.ScriptablePlayer;

namespace dragoni7
{
    public class PlayerController : Singletone<PlayerController>
    {
        private AbstractPlayer currentPlayer;
        public void SpawnPlayer(PlayerType type, Vector2 pos)
        {
            var scriptablePlayer = ResourceSystem.Instance.GetPlayer(type);

            AbstractPlayer spawned = Instantiate(scriptablePlayer.prefab, pos, Quaternion.identity, transform);

            // modify stats if needed
            var stats = scriptablePlayer.BaseStats;
            stats.health += 10;

            spawned.SetStats(stats);
            currentPlayer = spawned;
        }

        private void Update()
        {
            // execute updates on the current player
        }
    }
}

