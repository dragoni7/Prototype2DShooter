using Cinemachine;
using UnityEngine;
using Util;

namespace dragoni7
{

    /// <summary>
    /// Controller class for manipulating player
    /// </summary>
    public class PlayerController : Singleton<PlayerController>
    {
        public AbstractPlayer CurrentPlayer { get; set; }

        public CinemachineVirtualCamera playerCam;

        /// <summary>
        /// Spawns a player character
        /// </summary>
        /// <param name="name">name of player type</param>
        /// <param name="pos">position of spawn</param>
        public void SpawnPlayer(string name, Vector2 pos)
        {
            // spawn player
            PlayerData scriptablePlayer = ResourceSystem.Instance.GetPlayer(name);

            AbstractPlayer spawnedPlayer = Instantiate(scriptablePlayer.playerPrefab, pos, Quaternion.identity, transform);

            // modify stats if needed
            Attributes playerAttributes = scriptablePlayer.BaseAttributes;

            spawnedPlayer.SetAttributes(playerAttributes);
            spawnedPlayer.Abilities = scriptablePlayer.Abilities;

            // create player's weapon
            WeaponData scriptableWeapon = ResourceSystem.Instance.GetWeapon(scriptablePlayer.scriptableWeapon.name);
            AbstractWeapon spawnedWeapon = Instantiate(scriptablePlayer.scriptableWeapon.weaponPrefab, pos, Quaternion.identity, spawnedPlayer.EquipParent);

            // create weapon emitter
            EmitterData scriptableEmitter = ResourceSystem.Instance.GetEmitter(scriptableWeapon.scriptableEmitter.name);
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

            GameEventManager.Instance.EventBus.Raise(new PlayerSpawnEvent { player = CurrentPlayer });
        }

        /// <summary>
        /// Checks if a position is within 40 units of player
        /// </summary>
        /// <param name="position">position to check</param>
        /// <returns>true if near player, false otherwise</returns>
        public bool IsNearPlayer(Vector2 position)
        {
            return Vector2.Distance(position, CurrentPlayer.transform.position) < 40;
        }

        /// <summary>
        /// Performs the current players attack and adjusts speed
        /// </summary>
        public void PlayerAttack()
        {
            CurrentPlayer.CurrentSpeed = CurrentPlayer.CurrentAttributes.Get(AttributeType.AttackingSpeed);
            CurrentPlayer.PerformAttack();
        }
    }
}

