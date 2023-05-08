using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static dragoni7.ScriptablePlayer;

namespace dragoni7
{
    public class ResourceSystem : Singletone<ResourceSystem>
    {
        public List<ScriptablePlayer> ScriptablePlayers { get; private set; }
        private Dictionary<PlayerType, ScriptablePlayer> playersDict;

        protected override void Awake()
        {
            base.Awake();
            AssembleResources();
        }

        private void AssembleResources()
        {
            // Players
            ScriptablePlayers = Resources.LoadAll<ScriptablePlayer>("Players").ToList();
            playersDict = ScriptablePlayers.ToDictionary(r => r.playerType, r => r);

            // Enemies
        }

        public ScriptablePlayer GetPlayer(PlayerType t) => playersDict[t];
    }
}
