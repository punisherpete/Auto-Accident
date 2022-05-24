using Cinemachine;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class CamerasSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _projectorFollowAndLookAtCar;
    [SerializeField] private CinemachineVirtualCamera _projectorFollowAndLookAtProjector;
    [SerializeField] private Mover _carMover;
    [SerializeField] private SplineProjectorObserver _projectorObserver;
    [SerializeField] private float _offsetMistakeForZooming = 0.5f;
    [SerializeField] private float _actionDuration = 2f;
    [SerializeField] private float _rebindDelay = 2f;

    private float _timeSpent;
    private Respawner _carRespawner;
    private MMFeedbacks _accelerationFeedBacks; 

    private void Awake()
    {
        _carRespawner = _carMover.GetComponent<Respawner>();
        _accelerationFeedBacks = GetComponent<MMFeedbacks>();
    }

    private void OnEnable()
    {
        _carRespawner.Proceed += OnChangeCameraBodyBindingMode;
        _carMover.Boosted += OnPlayAccelerationEffect;
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
        _carMover.Boosted -= OnPlayAccelerationEffect;
    }

    private void Transit()
    {
        _timeSpent = 0f;
        float criticalOffset = _carMover.GetCriticalOffset() + _offsetMistakeForZooming;
        bool isSwitched = _projectorObserver.IsGoesBeyondCriticalDistance(criticalOffset);
        float targetOffsetZ = isSwitched ? -12 : -7;
        float currentOffsetZ = _projectorFollowAndLookAtProjector.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z;
        _projectorFollowAndLookAtProjector.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = Mathf.Lerp(currentOffsetZ, targetOffsetZ, Time.deltaTime * 10);
        //_projectorFollowAndLookAtCar.gameObject.SetActive(isSwitched);
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

    private void OnPlayAccelerationEffect()
    {
        _accelerationFeedBacks?.PlayFeedbacks();
    }
}
