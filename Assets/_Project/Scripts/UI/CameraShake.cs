using Cinemachine;
using UnityEngine;
using Utils;

namespace dragoni7
{
    public class CameraShake : Singleton<CameraShake>
    {
        private CinemachineVirtualCamera _camera;
        private float shakeTimer;
        private float shakerTimeTotal;
        private float startingIntensity;
        protected override void Awake()
        {
            base.Awake();
            _camera = GetComponent<CinemachineVirtualCamera>();
        }
        public void ShakeCamera(float intensity, float time)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

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
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 -( shakeTimer / shakerTimeTotal));
            }
        }
    }
}
