using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName ="New Scriptable Player")]
    public class ScriptablePlayer : ScriptableEntity
    {
        public AbstractPlayer playerPrefab;

        public ScriptableWeapon scriptableWeapon;
    }
}
