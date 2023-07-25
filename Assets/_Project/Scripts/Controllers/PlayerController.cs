using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using Utils;

namespace dragoni7
{
    public class PlayerController : Singletone<PlayerController>
    {
        public AbstractPlayer CurrentPlayer { get; set; }

        public CinemachineVirtualCamera playerCam;
        public void SpawnPlayer(string name, Vector2 pos)
        {
            // spawn player
            var scriptablePlayer = ResourceSystem.Instance.GetPlayer(name);

            AbstractPlayer spawnedPlayer = Instantiate(scriptablePlayer.playerPrefab, pos, Quaternion.identity, transform);

            // modify stats if needed
            var playerStats = scriptablePlayer.BaseStats;

            spawnedPlayer.SetStats(playerStats);
            spawnedPlayer.Abilities = scriptablePlayer.Abilities;

            // create player's weapon
            var scriptableWeapon = ResourceSystem.Instance.GetWeapon(scriptablePlayer.scriptableWeapon.name);
            AbstractWeapon spawnedWeapon = Instantiate(scriptablePlayer.scriptableWeapon.weaponPrefab, pos, Quaternion.identity, spawnedPlayer.EquipParent);

            // create weapon emitter
            var scriptableEmitter = ResourceSystem.Instance.GetEmitter(scriptableWeapon.scriptableEmitter.name);
            BaseEmitter spawnedEmitter = Instantiate(scriptableEmitter.emitterPrefab, pos, Quaternion.identity, spawnedWeapon.EmitPoint);
            spawnedEmitter.transform.localPosition = Vector3.zero;
            spawnedEmitter.transform.localRotation = Quaternion.Euler(0, 0, -90);
            spawnedEmitter.SetStats(scriptableEmitter.BaseStats);
            spawnedEmitter.pattern = scriptableEmitter.patternPrefab;
            spawnedEmitter.Bullet = scriptableEmitter.scriptableBullet;

            // set weapon emitter
            spawnedWeapon.Emitter = spawnedEmitter;

            // set player weapon
            spawnedPlayer.Weapon = spawnedWeapon;

            // set current player
            CurrentPlayer = spawnedPlayer;
            playerCam.LookAt = CurrentPlayer.transform;
            playerCam.Follow = CurrentPlayer.transform;
        }

        public bool IsNearPlayer(Vector2 position)
        {
            return Vector2.Distance(position, CurrentPlayer.transform.position) < 40;
        }

        public void DamagePlayer(int damage)
        {
            //CurrentPlayer.TakeDamage(damage);
        }
    }
}

