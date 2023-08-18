using UnityEngine;

namespace dragoni7
{
    public class Item : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnPlayerPickup(collision.GetComponentInParent<AbstractPlayer>());
            }
        }

        protected virtual void OnPlayerPickup(AbstractPlayer player)
        {
            Destroy(gameObject);
        }
    }
}
