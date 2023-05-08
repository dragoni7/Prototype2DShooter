using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace dragoni7
{
    public class Player1 : AbstractPlayer
    {
        private bool canDash = true;
        private bool isDashing;
        [SerializeField] private float dashingPower = 24f;
        [SerializeField] private float dashingTime = 0.2f;
        [SerializeField] private float dashingCooldown = 1f;
        public void Start()
        {
            print("In player1:" + Stats.speed);
            Vector2 gunPosition = (Vector2)transform.position + equipPos;
            gun = Instantiate(gunPrefab, gunPosition, Quaternion.identity);
            gun.cam = FindAnyObjectByType<Camera>();
            gun.transform.position = gunPosition;
        }
        public override void OnAbility1Use(InputAction.CallbackContext context)
        {
            if (context.performed && canDash)
            {
                StartCoroutine(Dash());
            }
        }

        public override void TakeDamage(int damage)
        {
            
        }

        void Update()
        {

            if (isFiring)
            {
                gun.Shoot();
                currentSpeed = Stats.shootingSpeed;
            }
            else
            {
                currentSpeed = Stats.speed;
            }

            Vector2 moveVelocity = currentSpeed * (
                currentMove.x * Vector2.right +
                currentMove.y * Vector2.up
            );

            Vector3 moveThisFrame = Time.deltaTime * moveVelocity;
            transform.position += moveThisFrame;
            gun.transform.position = (Vector2)transform.position + equipPos;
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
