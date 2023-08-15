using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public class PlayerController : Singleton<PlayerController>
    {
        public AbstractPlayer CurrentPlayer { get; set; }

        public CinemachineVirtualCamera playerCam;
        public void SpawnPlayer(string name, Vector2 pos)
        {
            // spawn player
            var scriptablePlayer = ResourceSystem.Instance.GetPlayer(name);

            AbstractPlayer spawnedPlayer = Instantiate(scriptablePlayer.playerPrefab, pos, Quaternion.identity, transform);

            // modify stats if needed
            var playerStats = scriptablePlayer.BaseAttributes;

            spawnedPlayer.SetAttributes(playerStats);
            spawnedPlayer.Abilities = scriptablePlayer.Abilities;

            // create player's weapon
            var scriptableWeapon = ResourceSystem.Instance.GetWeapon(scriptablePlayer.scriptableWeapon.name);
            AbstractWeapon spawnedWeapon = Instantiate(scriptablePlayer.scriptableWeapon.weaponPrefab, pos, Quaternion.identity, spawnedPlayer.EquipParent);

            // create weapon emitter
            var scriptableEmitter = ResourceSystem.Instance.GetEmitter(scriptableWeapon.scriptableEmitter.name);
            BaseEmitter spawnedEmitter = Instantiate(scriptableEmitter.emitterPrefab, pos, Quaternion.identity, spawnedWeapon.EmitPoint);
            spawnedEmitter.transform.localPosition = Vector3.zero;
            spawnedEmitter.SetAttributes(scriptableEmitter.BaseAttributes);
            spawnedEmitter.Pattern = Instantiate(scriptableEmitter.patternPrefab, spawnedEmitter.transform.position, Quaternion.identity, spawnedEmitter.transform);
            spawnedEmitter.Bullet = scriptableEmitter.scriptableBullet;

            // set weapon emitter
            spawnedWeapon.Emitter = spawnedEmitter;

            // set player weapon
            spawnedPlayer.Weapon = spawnedWeapon;

            // set current player
            CurrentPlayer = spawnedPlayer;
            playerCam.LookAt = CurrentPlayer.transform;
            playerCam.Follow = CurrentPlayer.transform;

            EventSystem.Instance.TriggerEvent(Events.OnPlayerSpawned, new Dictionary<string, object> { { "Player", CurrentPlayer } });
        }
        public bool IsNearPlayer(Vector2 position)
        {
            return Vector2.Distance(position, CurrentPlayer.transform.position) < 40;
        }
        public void PlayerAttack()
        {
            CurrentPlayer.CurrentSpeed = CurrentPlayer.Attributes.shootingSpeed;
            CurrentPlayer.PerformAttack();
        }
    }
}

