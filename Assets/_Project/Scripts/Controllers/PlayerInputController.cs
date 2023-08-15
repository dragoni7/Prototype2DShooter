using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace dragoni7
{
    public class PlayerInputController : Singleton<PlayerInputController>
    {
        private bool isAttacking = false;
        private Vector2 currentMove;
        private PlayerController pController;
        public Camera MainCamera { get; private set; }

        private void Start()
        {
            pController = PlayerController.Instance;
            MainCamera = FindAnyObjectByType<Camera>();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            currentMove = context.ReadValue<Vector2>();
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            isAttacking = context.ReadValueAsButton();
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
            if (GameController.Instance.CurrentState == GameController.GameState.PlayingLevel)
            {
                if (Input.GetAxis("Mouse ScrollWheel") != 0)
                {
                    UIController.Instance.ZoomPlayerCamera(Input.GetAxis("Mouse ScrollWheel"));
                }
            }
        }
        public void FixedUpdate()
        {
            AbstractPlayer player = pController.CurrentPlayer;

            if (GameController.Instance.CurrentState == GameController.GameState.PlayingLevel)
            {

                if (!player.CanAttack && !player.CanMove)
                {
                    return;
                }

                if (isAttacking && player.CanAttack)
                {
                    EventSystem.Instance.TriggerEvent(Events.OnEntityAttack, new Dictionary<string, object> { { "Entity", player} });
                }
                else
                {
                    player.CurrentSpeed = player.Attributes.speed;
                }

                if (player.CanMove)
                {
                    // Move player
                    player.rb.velocity = currentMove * player.CurrentSpeed;

                    // Rotate player's weapon
                    Vector3 mousePosition = MainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    Vector3 lookDirection = mousePosition - player.Weapon.transform.position;
                    float aimAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;

                    player.Weapon.transform.rotation = Quaternion.Euler(0, 0, aimAngle);
                }
            }
        }
    }
}

