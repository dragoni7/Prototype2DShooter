using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace dragoni7
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float baseMoveSpeed = 5f;
        public float shootingMoveSpeed = 3f;
        public Transform equipPos;
        private bool isFiring = false;
        private Vector2 currentMove;

        private float currentSpeed = 5f;

        public Camera cam;
        public PlayerGun gunPrefab;
        private PlayerGun gun;

        private bool canDash = true;
        private bool isDashing;
        private float dashingPower = 24f;
        private float dashingTime = 0.2f;
        private float dashingCooldown = 1f;

        public void Start()
        {
            gun = Instantiate(gunPrefab, transform.position, transform.rotation);
            gun.cam = FindAnyObjectByType<Camera>();
            gun.transform.position = equipPos.position;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            currentMove = context.ReadValue<Vector2>();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            isFiring = context.ReadValueAsButton();
        }

        public void OnAbility1Use(InputAction.CallbackContext context)
        {
            if (context.performed && canDash)
            {
                StartCoroutine(Dash());
            }
        }
        void Update()
        {

            if (isFiring)
            {
                gun.Shoot();
                currentSpeed = shootingMoveSpeed;
            }
            else
            {
                currentSpeed = baseMoveSpeed;
            }

            Vector2 moveVelocity = currentSpeed * (
                currentMove.x * Vector2.right +
                currentMove.y * Vector2.up
            );

            Vector3 moveThisFrame = Time.deltaTime * moveVelocity;
            transform.position += moveThisFrame;
            gun.transform.position = equipPos.position;
        }

        private void FixedUpdate()
        {
            if (isDashing)
            {
                return;
            }
        }

        private IEnumerator Dash()
        {
            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);

            yield return new WaitForSeconds(dashingTime);
            rb.gravityScale = originalGravity;
            isDashing = false;
            rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}
