using dragoni;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace dragoni7
{
    public class Bullet : MonoBehaviour
    {
        public Vector2 Velocity { get; set; }
        public float BulletForce { get; set; }
        public DamageModifiers CurrentDamageModifier { get; set; }
        public BulletAttributes Attributes { get; protected set; }
        public IDamage DamageType { get; set; }

        [SerializeField] private List<string> _ignoreTags;

        private int _timer;

        public virtual void Start()
        {
            _timer = 0;
        }

        public void SetAttributes(BulletAttributes attributes)
        {
            DamageType = DamageFactory.GetDamage(attributes.baseDamageType);
            GetComponent<SpriteRenderer>().color = DamageType.GetColor();
            Attributes = attributes;
        }
        public virtual void Update()
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 newPosition = currentPosition + BulletForce * Time.deltaTime * Velocity;

            RaycastHit2D[] hits = Physics2D.LinecastAll(currentPosition, newPosition);

            for (int i = 0; i < hits.Count(); i++)
            {
                GameObject hitObject = hits[i].collider.gameObject;

                foreach (string tag in _ignoreTags)
                {
                    if (!hitObject.CompareTag(tag))
                    {
                        // hit logic
                        if (gameObject.activeSelf)
                        {
                            _timer = 0;
                            gameObject.SetActive(false);

                            if (hitObject.TryGetComponent(out Entity target))
                            {
                                EventSystem.Instance.TriggerEvent(EventSystem.Events.OnEntityDamaged, new Dictionary<string, object> { { "damage", DamageType }, { "damageModifier", CurrentDamageModifier }, { "target", target }, { "source", gameObject } });
                            }
                        }
                    }
                }
            }

            transform.position = newPosition;
        }
        public virtual void FixedUpdate()
        {
            if (_timer >= Attributes.duration)
            {
                _timer = 0;
                gameObject.SetActive(false);
            }

            _timer++;
        }
    }
}
