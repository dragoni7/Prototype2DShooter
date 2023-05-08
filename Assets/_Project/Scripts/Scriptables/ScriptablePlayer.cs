using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dragoni7
{

    [CreateAssetMenu(fileName ="New Scriptable Player")]
    public class ScriptablePlayer : AbstractScriptableEntity
    {
        public PlayerType playerType;

        public AbstractPlayer prefab;

        [Serializable]
        public enum PlayerType
        {
            Player1 = 0,
            Player2 = 1,
            Player3 = 2,
        }
    }
}
