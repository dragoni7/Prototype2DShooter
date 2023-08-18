using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;
using Utils;

namespace dragoni7
{
    public class PlayerInputController : Singleton<PlayerInputController>
    {
        private bool _isAttacking = false;
        private Vector2 _currentMove;
        private PlayerController _pController;

        private List<ITask> _playerInputTasks = new();
        public Vector2 CurrentMove => _currentMove;
        public bool IsAttacking => _isAttacking;

        private void Start()
        {
            _pController = PlayerController.Instance;
            _currentMove = Vector2.zero;
            _playerInputTasks.Add(new PlayerAttackTask());
            _playerInputTasks.Add(new MovePlayerTask());
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            _currentMove = context.ReadValue<Vector2>();
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            _isAttacking = context.ReadValueAsButton();
        }

        public void OnAbility1(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(_pController.CurrentPlayer.Abilities[0].Execute(_pController.CurrentPlayer));
            }
        }

        public void OnAbility2(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(_pController.CurrentPlayer.Abilities[1].Execute(_pController.CurrentPlayer));
            }
        }

        public void OnAbility3(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(_pController.CurrentPlayer.Abilities[2].Execute(_pController.CurrentPlayer));
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
            AbstractPlayer player = _pController.CurrentPlayer;

            if ((GameController.Instance.CurrentState == GameController.GameState.PlayingLevel) && !(!player.CanAttack && !player.CanMove))
            {
                foreach (ITask task in _playerInputTasks)
                {
                    task.Execute();
                }
            }
        }
    }
}

