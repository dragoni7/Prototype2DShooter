using Cinemachine;
using UnityEngine;
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
        private CinemachineVirtualCamera _playerCam;

        [SerializeField]
        private float _zoomSensitivity = 10f;

        public float playerCamZoom;
        private float shakeTimer;
        private float shakerTimeTotal;
        private float startingIntensity;
        protected override void Awake()
        {
            base.Awake();
            playerCamZoom = _playerCam.m_Lens.OrthographicSize;
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
            floatingText.SetText(value, color, playerCamZoom);
        }
        public void ZoomPlayerCamera(float axisValue)
        {
            float _orthoSize = axisValue * _zoomSensitivity;

            float newSize = _orthoSize + _playerCam.m_Lens.OrthographicSize;

            if (newSize > 3 && newSize < 18)
            {
                _playerCam.m_Lens.OrthographicSize = newSize;
                playerCamZoom = newSize;
            }
        }

        public void ShakePlayerCamera(float intensity, float time)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _playerCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            startingIntensity = intensity;
            shakerTimeTotal = time;
            shakeTimer = time;
        }
        private void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _playerCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakerTimeTotal));
            }
        }
    }
}
