using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public class BulletController : Singleton<BulletController>
    {
        [SerializeField] List<GameObject> bulletPrefabs;
        private Dictionary<string, ObjectPool<PoolObject>> bulletPools = new();

        private void Start()
        {
            // create a pool for each bullet type
            foreach(var bulletType in bulletPrefabs)
            {
                bulletPools.Add(bulletType.name, new ObjectPool<PoolObject>(bulletType, 1));
            }
        }

        public void SpawnBullet(BulletData scriptableBullet, Vector2 position, Quaternion rotation, Vector2 velocity, float force, DamageModifiers damageModifier)
        {
            bulletPools.TryGetValue(scriptableBullet.bulletPrefab.name, out var pool);
            GameObject bullet = pool.PullGameObject(position, rotation);
            bullet.GetComponent<Bullet>().SetAttributes(scriptableBullet.BaseAttributes);
            bullet.GetComponent<Bullet>().Velocity = velocity;
            bullet.GetComponent<Bullet>().BulletForce = force;
            bullet.GetComponent<Bullet>().CurrentDamageModifier = damageModifier;
        }
    }
}
