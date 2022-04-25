using Cinemachine;
using UnityEngine;

public class CamerasSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _projectorFollowAndLookAtCar;
    [SerializeField] private SplineProjectorObserver _projectorObserver;
    [SerializeField] private float _criticalOffset = 6f;

    private void LateUpdate()
    {
        bool isSwitched = _projectorObserver.IsGoesBeyondCriticalDistance(_criticalOffset);
        _projectorFollowAndLookAtCar.gameObject.SetActive(isSwitched);
    }
}
