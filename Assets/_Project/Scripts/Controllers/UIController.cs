using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace dragoni7
{

    /// <summary>
    /// Controller class for handling ui manipulations
    /// </summary>
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

        private readonly List<ITask> _uiTasks = new();

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

        /// <summary>
        /// Updates the screen health bar's value
        /// </summary>
        /// <param name="value">new value</param>
        public void UpdatePlayerHealthBar(float value)
        {
            _playerHealthBar.SetHealth(value);
        }

        /// <summary>
        /// Updates the screen health bar's max value
        /// </summary>
        /// <param name="value">new value</param>
        public void UpdatePlayerHealthBarMaxHP(float value)
        {
            _playerHealthBar.SetMaxHealth(value);
        }

        /// <summary>
        /// Adds a entity's health bar to the canvas
        /// </summary>
        /// <param name="entity">entity with healthbar</param>
        public void AddEntityHealthBar(Entity entity)
        {
            entity.HealthBar.transform.SetParent(_worldCanvas.transform);
            entity.HealthBar.transform.position = entity.transform.position + (Vector3.up * 0.5f);
        }

        /// <summary>
        /// Spawns a floating message
        /// </summary>
        /// <param name="position">position of spawn</param>
        /// <param name="value">message value</param>
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

        /// <summary>
        /// Zooms the player cam
        /// </summary>
        /// <param name="axisValue">mouse wheel zoom value</param>
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

        /// <summary>
        /// Shakes the player cam
        /// </summary>
        /// <param name="intensity">shake intensity</param>
        /// <param name="time">shake time</param>
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
