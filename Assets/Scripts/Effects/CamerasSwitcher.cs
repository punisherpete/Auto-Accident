using Cinemachine;
using UnityEngine;

public class CamerasSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _projectorFollowAndLookAtCar;
    [SerializeField] private Mover _carMover;
    [SerializeField] private SplineProjectorObserver _projectorObserver;

    private void LateUpdate()
    {
        bool isSwitched =  _projectorObserver.IsGoesBeyondCriticalDistance(_carMover.GetCriticalOffset() - 0.5f);
        _projectorFollowAndLookAtCar.gameObject.SetActive(isSwitched);
    }
}
