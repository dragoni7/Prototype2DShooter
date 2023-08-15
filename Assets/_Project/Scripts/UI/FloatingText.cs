using TMPro;
using UnityEngine;

namespace dragoni7
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rb;
        [SerializeField]
        private TMP_Text _damageValue;

        public float initialYVelocity = 7f;
        public float initialXVelocityRange = 3f;
        public float LifeTime = 0.8f;

        private void Start()
        {
            _rb.velocity = new Vector2(Random.Range(-initialXVelocityRange, initialXVelocityRange), initialYVelocity);
            Destroy(gameObject, LifeTime);
        }

        public void SetText(string value)
        {
            _damageValue.SetText(value);
        }
        public void SetText(string value, Color color, float size)
        {
            _damageValue.SetText(value);
            _damageValue.color = color;
            _damageValue.fontSize = (0.08f * size);
        }
    }
}
