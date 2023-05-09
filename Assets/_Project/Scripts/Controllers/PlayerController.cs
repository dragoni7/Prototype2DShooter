using UnityEngine;

namespace dragoni7
{
    public class PlayerController : Singletone<PlayerController>
    {
        public AbstractPlayer CurrentPlayer { get; set; }
        public void SpawnPlayer(string name, Vector2 pos)
        {
            // spawn player
            var scriptablePlayer = ResourceSystem.Instance.GetPlayer(name);

            AbstractPlayer spawnedPlayer = Instantiate(scriptablePlayer.playerPrefab, pos, Quaternion.identity, transform);

            // modify stats if needed
            var playerStats = scriptablePlayer.BaseStats;
            playerStats.health += 10;

            spawnedPlayer.SetStats(playerStats);
            spawnedPlayer.Abilities = scriptablePlayer.Abilities;
            spawnedPlayer.EquipPos = scriptablePlayer.equipPos;

            // create player's gun
            var scriptableWeapon = ResourceSystem.Instance.GetWeapon(scriptablePlayer.scriptableWeapon.name);
            AbstractWeapon spawnedWeapon = Instantiate(scriptablePlayer.scriptableWeapon.weaponPrefab, pos, Quaternion.identity, transform);
            var weaponStats = scriptableWeapon.BaseStats;
            spawnedWeapon.SetStats(weaponStats);

            // set bullet for gun
            spawnedWeapon.Bullet = ResourceSystem.Instance.GetBullet(scriptableWeapon.scriptableBullet.name);

            // set player gun
            spawnedPlayer.Weapon = spawnedWeapon;

            // set current player
            CurrentPlayer = spawnedPlayer;
        }

        public void DamagePlayer(int damage)
        {
            CurrentPlayer.TakeDamage(damage);
        }
    }
}

