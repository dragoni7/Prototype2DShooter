using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace dragoni7
{
    public class MovePlayerTask : ITask
    {
        public void Execute()
        {
            AbstractPlayer player = PlayerController.Instance.CurrentPlayer;

            if (player.CanMove)
            {
                // Move player
                player.rb.velocity = PlayerInputController.Instance.CurrentMove * player.CurrentSpeed;

                // Rotate player's weapon
                Vector3 mousePosition = UIController.Instance.mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                Vector3 lookDirection = mousePosition - player.Weapon.transform.position;
                float aimAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

                player.Weapon.transform.rotation = Quaternion.Euler(0, 0, aimAngle);
            }
        }
    }
}
