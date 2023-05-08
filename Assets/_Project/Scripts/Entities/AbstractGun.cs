using UnityEngine;

namespace dragoni7
{
    public abstract class AbstractGun : MonoBehaviour
    {
        public Rigidbody2D rigidBody;
        public GameObject bulletPrefab;
        public Transform spawnPoint;
        public int attackCooldown;
        public float bulletForce;
        protected int attackCounter;
        public abstract void Shoot();
        public abstract void FixedUpdate();
        public abstract void Update();
    }
}
