using UnityEngine;
using Utils;

namespace dragoni7
{
    public class UIController : Singleton<UIController>
    {
        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private Canvas _uiCanvas;

        [SerializeField]
        private HealthBar _playerHealthBar;

        public void UpdatePlayerHealthBar(float value)
        {
            _playerHealthBar.SetHealth(value);
        }

        public void UpdatePlayerHealthBarMaxHP(float value)
        {
            _playerHealthBar.SetMaxHealth(value);
        }

        public void AddEntityHealthBar(Entity entity)
        {
            entity.HealthBar.transform.SetParent(_canvas.transform);
            entity.HealthBar.transform.position = entity.transform.position + (Vector3.up * 0.5f);
        }
    }
}
