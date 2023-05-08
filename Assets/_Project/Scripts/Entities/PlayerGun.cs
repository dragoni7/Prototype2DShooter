using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace dragoni7
{
    public class PlayerGun : AbstractGun
    {
        public Camera cam;
        private Vector2 mousePosition;
        public override void FixedUpdate()
        {
            Vector2 lookDir = mousePosition - rigidBody.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            rigidBody.rotation = angle;
        }

        public override void Shoot()
        {
            if (attackCounter == attackCooldown)
            {
                GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(spawnPoint.up * bulletForce, ForceMode2D.Impulse);
                attackCounter = 0;
            }

            attackCounter++;
        }

        public override void Update()
        {
            mousePosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}
