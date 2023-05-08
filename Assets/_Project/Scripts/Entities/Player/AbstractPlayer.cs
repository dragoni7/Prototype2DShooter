using UnityEngine.InputSystem;
using UnityEngine;
using static dragoni7.GameController;
using static dragoni7.AbstractScriptableEntity;

namespace dragoni7
{
    public abstract class AbstractPlayer : Entity
    {
        public Rigidbody2D rb;
        public Vector2 equipPos;
        public PlayerGun gunPrefab;

        protected Camera cam;
        protected Vector2 currentMove;
        protected bool isFiring = false;
        protected float currentSpeed;
        protected PlayerGun gun;
        public void OnMove(InputAction.CallbackContext context)
        {
            currentMove = context.ReadValue<Vector2>();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            isFiring = context.ReadValueAsButton();
        }
        public abstract void OnAbility1Use(InputAction.CallbackContext context);
        private void OnStateChanged(GameState newState)
        {
            // change logic according to state
        }

        private void Awake()
        {
            // Subscribe events
            GameController.OnBeforeStateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            // Unsubscribe events
            GameController.OnBeforeStateChanged -= OnStateChanged;
        }
    }
}
