using Cinemachine;
using UnityEngine;

public class CamerasSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _projectorFollowAndLookAtCar;
    [SerializeField] private Mover _carMover;
    [SerializeField] private float _criticalOffset = 6f;

    private void LateUpdate()
    {
        bool isSwitched = _carMover.GetCriticalOffset() > _criticalOffset;
        _projectorFollowAndLookAtCar.gameObject.SetActive(isSwitched);
    }
}
