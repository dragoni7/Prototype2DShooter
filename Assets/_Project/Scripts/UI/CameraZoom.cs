using UnityEngine;
using Cinemachine;

namespace dragoni7
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField]
        CinemachineVirtualCamera virtualCamera;

        float orthoSize;
        [SerializeField]
        float sensitivity = 10f;

        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                orthoSize = Input.GetAxis("Mouse ScrollWheel") * sensitivity;

                float newSize = orthoSize + virtualCamera.m_Lens.OrthographicSize;

                if (newSize > 3 && newSize < 18)
                {
                    virtualCamera.m_Lens.OrthographicSize = newSize;
                }
            }
        }
    }
}
