using Cinemachine;
using UnityEngine;
using Util;

namespace dragoni7
{
    public class CameraShakeTask : ITask
    {
        public void Execute()
        {
            UIController controller = UIController.Instance;

            if (controller.ShakeTimer > 0)
            {
                controller.ShakeTimer -= Time.deltaTime;
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = controller.playerCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(controller.StartingIntensity, 0f, 1 - (controller.ShakeTimer / controller.ShakerTimeTotal));
            }
        }
    }
}
