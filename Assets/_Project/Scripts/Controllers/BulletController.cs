using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public class BulletController : Singletone<BulletController>
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

        public void SpawnBullet(BulletData scriptableBullet, Vector2 position, Quaternion rotation, Vector2 velocity, float force)
        {
            bulletPools.TryGetValue(scriptableBullet.bulletPrefab.name, out var pool);
            GameObject bullet = pool.PullGameObject(position, rotation);
            bullet.GetComponent<AbstractBullet>().SetStats(scriptableBullet.BaseStats);
            bullet.GetComponent<AbstractBullet>().Velocity = velocity;
            bullet.GetComponent<AbstractBullet>().BulletForce = force;
            //bullet.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        }
    }
}
