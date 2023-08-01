using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Weapon")]
    public class WeaponData : AbstractData
    {
        public AbstractWeapon weaponPrefab;
        public EmitterData scriptableEmitter;
    }
}
