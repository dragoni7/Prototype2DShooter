using System;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Weapon")]
    public class ScriptableWeapon : AbstractScriptable
    {
        public AbstractWeapon weaponPrefab;
        public ScriptableEmitter scriptableEmitter;
    }
}
