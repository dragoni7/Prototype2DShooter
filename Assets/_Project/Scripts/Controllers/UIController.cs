using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using Util;
using Utils;

namespace dragoni7
{
    public class UIController : Singleton<UIController>
    {
        [SerializeField]
        private Canvas _worldCanvas;

        [SerializeField]
        private Canvas _playerCanvas;

        [SerializeField]
        private HealthBar _playerHealthBar;

        [SerializeField]
        private FloatingText _floatingText;

        [SerializeField]
        private float _zoomSensitivity = 10f;

        private List<ITask> _uiTasks = new();

        private float _shakerTimeTotal;
        private float _startingIntensity;
        public float ShakerTimeTotal => _shakerTimeTotal;
        public float StartingIntensity => _startingIntensity;
        public float PlayerCamZoom { get; set; }
        public float ShakeTimer { get; set; }

        public CinemachineVirtualCamera playerCam;
        public Camera mainCamera;

        private void Start()
        {
            PlayerCamZoom = playerCam.m_Lens.OrthographicSize;
            _uiTasks.Add(new CameraShakeTask());
        }
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
            entity.HealthBar.transform.SetParent(_worldCanvas.transform);
            entity.HealthBar.transform.position = entity.transform.position + (Vector3.up * 0.5f);
        }
        public void SendFloatingMessage(Vector2 position, string value)
        {
            SendFloatingMessage(position, value, Color.white);
        }
        public void SendFloatingMessage(Vector2 position, string value, Color color)
        {
            var messageObj = Instantiate(_floatingText, position, Quaternion.identity, _worldCanvas.transform);
            var floatingText = messageObj.GetComponent<FloatingText>();
            floatingText.SetText(value, color, PlayerCamZoom);
        }
        public void ZoomPlayerCamera(float axisValue)
        {
            float _orthoSize = axisValue * _zoomSensitivity;

            float newSize = _orthoSize + playerCam.m_Lens.OrthographicSize;

            if (newSize > 3 && newSize < 18)
            {
                playerCam.m_Lens.OrthographicSize = newSize;
                PlayerCamZoom = newSize;
            }
        }
        public void ShakePlayerCamera(float intensity, float time)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = playerCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            _startingIntensity = intensity;
            _shakerTimeTotal = time;
            ShakeTimer = time;
        }
        private void Update()
        {
            if (GameController.Instance.CurrentState == GameController.GameState.PlayingLevel)
            {
                foreach (ITask task in _uiTasks)
                {
                    task.Execute();
                }
            }
        }
    }
}
