using Cinemachine;
using System.Collections;
using UnityEngine;

public class CamerasSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _projectorFollowAndLookAtCar;
    [SerializeField] private CinemachineVirtualCamera _projectorFollowAndLookAtProjector;
    [SerializeField] private Mover _carMover;
    [SerializeField] private SplineProjectorObserver _projectorObserver;
    [SerializeField] private float _offsetMistake = 0.5f;
    [SerializeField] private float _actionDuration = 2f;
    [SerializeField] private float _rebindDelay = 2f;

    private float _timeSpent;
    private Respawner _carRespawner;

    private void Awake()
    {
        _carRespawner = _carMover.GetComponent<Respawner>();
    }

    private void OnEnable()
    {
        _carRespawner.Proceed += OnChangeCameraBodyBindingMode;
    }

    private void Update()
    {
        _timeSpent += Time.deltaTime;

        if (_timeSpent > _actionDuration)
        {
            Transit();
        }
    }

    private void OnDisable()
    {
        _carRespawner.Proceed -= OnChangeCameraBodyBindingMode;
    }

    private void Transit()
    {
        _timeSpent = 0f;
        float criticalOffset = _carMover.GetCriticalOffset() + _offsetMistake;
        bool isSwitched = _projectorObserver.IsGoesBeyondCriticalDistance(criticalOffset);
        _projectorFollowAndLookAtCar.gameObject.SetActive(isSwitched);
    }

    private void OnChangeCameraBodyBindingMode()
    {
        StartCoroutine(RebindCameras(_rebindDelay));
    }

    private IEnumerator RebindCameras(float delay)
    {
        _projectorFollowAndLookAtCar.GetCinemachineComponent<CinemachineTransposer>().m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp; 
        _projectorFollowAndLookAtProjector.GetCinemachineComponent<CinemachineTransposer>().m_BindingMode = CinemachineTransposer.BindingMode.LockToTargetWithWorldUp;
        yield return new WaitForSeconds(delay);
        _projectorFollowAndLookAtCar.GetCinemachineComponent<CinemachineTransposer>().m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
        _projectorFollowAndLookAtProjector.GetCinemachineComponent<CinemachineTransposer>().m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
    }
}
