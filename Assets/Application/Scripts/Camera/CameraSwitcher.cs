using UnityEngine;
using Cinemachine;
using static System.TimeZoneInfo;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _bossFightCamera;

    private float _transitionDuration = 2f;
    private float _transitionTimer = 0f;
    private bool _isNeedSwitch = false;

    private void Update()
    {
        if (_isNeedSwitch)
        {
            SwitchCamera();

            if (_transitionTimer > 0f)
            {
                _transitionTimer -= Time.deltaTime;

                // ”станавливаем прогресс перехода дл€ обоих камер
                _playerCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(Vector3.zero, Vector3.back * 10f, 1f - (_transitionTimer / _transitionDuration));
                _bossFightCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(Vector3.back * 10f, Vector3.zero, 1f - (_transitionTimer / _transitionDuration));
            }
        }
    }

    private void SwitchCamera()
    {
        _transitionTimer = _transitionDuration;

        _bossFightCamera.gameObject.SetActive(true);
        _playerCamera.gameObject.SetActive(false);
    }
}
