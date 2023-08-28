using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName ="New Scriptable Player")]
    public class PlayerData : EntityData
    {
        public AbstractPlayer playerPrefab;

        public WeaponData scriptableWeapon;
    }
}
