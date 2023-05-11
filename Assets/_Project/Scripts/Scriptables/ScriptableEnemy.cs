using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Enemy")]
    public class ScriptableEnemy : ScriptableEntity
    {
        public AbstractEnemy enemyPrefab;
        public ScriptableEmitter scriptableEmitter;
    }
}
