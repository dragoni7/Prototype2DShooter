using System.Collections.Generic;
using UnityEngine;
using Util;

namespace dragoni7
{
    /// <summary>
    /// Controller class for bullet objects. Handles bullet manipulations
    /// </summary>
    public class BulletController : Singleton<BulletController>
    {
        [SerializeField] 
        List<GameObject> bulletPrefabs;

        private Dictionary<string, ObjectPool<PoolObject>> _bulletPools = new();

        private void Start()
        {
            // create a pool for each bullet type
            foreach(var bulletType in bulletPrefabs)
            {
                _bulletPools.Add(bulletType.name, new ObjectPool<PoolObject>(bulletType, 1));
            }
        }

        /// <summary>
        /// Instantiates a new bullet object
        /// </summary>
        /// <param name="scriptableBullet">bullet data</param>
        /// <param name="position">position to spawn from</param>
        /// <param name="rotation">initial rotation</param>
        /// <param name="velocity">initial velocity</param>
        /// <param name="force">force to apply to bullet</param>
        /// <param name="parentAttributes">parent entity attributes</param>
        public void SpawnBullet(BulletData scriptableBullet, Vector2 position, Quaternion rotation, Vector2 velocity, float force, Attributes parentAttributes)
        {
            _bulletPools.TryGetValue(scriptableBullet.bulletPrefab.name, out var pool);
            GameObject bulletObj = pool.PullGameObject(position, rotation);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.SetAttributes(scriptableBullet.BaseAttributes);
            bullet.ParentAttributes = parentAttributes;
            bullet.Velocity = velocity;
            bullet.BulletForce = force;
        }
    }
}
