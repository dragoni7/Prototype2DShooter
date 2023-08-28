using dragoni;
using UnityEngine;

namespace dragoni7
{
    [CreateAssetMenu(fileName = "New Scriptable Bullet")]
    public class BulletData : ScriptableObject
    {
        public Bullet bulletPrefab;

        [SerializeField] private BulletAttributes _baseAttributes;
        public BulletAttributes BaseAttributes => _baseAttributes;
    }
}
