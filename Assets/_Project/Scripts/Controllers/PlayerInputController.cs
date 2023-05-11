using UnityEngine;
using UnityEngine.InputSystem;

namespace dragoni7
{
    public class PlayerInputController : Singletone<PlayerInputController>
    {
        private bool isFiring = false;
        private Vector2 moveThisFrame;
        private Vector2 currentMove;
        private PlayerController pController;

        private void Start()
        {
            pController = PlayerController.Instance;
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            currentMove = context.ReadValue<Vector2>();
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            isFiring = context.ReadValueAsButton();
        }

        public void OnAbility1(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(pController.CurrentPlayer.Abilities[0].Execute(pController.CurrentPlayer));
            }
        }

        public void OnAbility2(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(pController.CurrentPlayer.Abilities[1].Execute(pController.CurrentPlayer));
            }
        }

        public void OnAbility3(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(pController.CurrentPlayer.Abilities[2].Execute(pController.CurrentPlayer));
            }
        }

        public void Update()
        {
            if (isFiring && pController.CurrentPlayer.canAttack)
            {
                pController.CurrentPlayer.CurrentSpeed = pController.CurrentPlayer.Stats.shootingSpeed;
            }
            else
            {
                pController.CurrentPlayer.CurrentSpeed = pController.CurrentPlayer.Stats.speed;
            }

            Vector2 moveVelocity = pController.CurrentPlayer.CurrentSpeed * (
                currentMove.x * Vector2.right +
                currentMove.y * Vector2.up
            );

            moveThisFrame = Time.deltaTime * moveVelocity;

            if (pController.CurrentPlayer.canMove)
            {
                pController.CurrentPlayer.Move(moveThisFrame);
            }
        }

        public void FixedUpdate()
        {
            if (!pController.CurrentPlayer.canAttack && !pController.CurrentPlayer.canMove)
            {
                return;
            }

            if (isFiring && pController.CurrentPlayer.canAttack)
            {
                pController.CurrentPlayer.Weapon.PerformAttack();
            }
        }
    }
}

