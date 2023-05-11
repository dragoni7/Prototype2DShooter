using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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

            // create player's weapon
            var scriptableWeapon = ResourceSystem.Instance.GetWeapon(scriptablePlayer.scriptableWeapon.name);
            AbstractWeapon spawnedWeapon = Instantiate(scriptablePlayer.scriptableWeapon.weaponPrefab, pos, Quaternion.identity, transform);

            // create weapon emitter
            var scriptableEmitter = ResourceSystem.Instance.GetEmitter(scriptableWeapon.scriptableEmitter.name);
            BaseEmitter spawnedEmitter = Instantiate(scriptableEmitter.emitterPrefab, pos, Quaternion.identity, transform);
            spawnedEmitter.SetStats(scriptableEmitter.BaseStats);
            spawnedEmitter.pattern = scriptableEmitter.patternPrefab;
            spawnedEmitter.Bullet = scriptableEmitter.scriptableBullet;

            // set weapon emitter
            spawnedWeapon.Emitter = spawnedEmitter;

            // set player weapon
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

