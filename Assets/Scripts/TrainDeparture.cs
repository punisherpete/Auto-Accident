using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TrainDeparture : MonoBehaviour
{
    [SerializeField] private float _traiMaxSpeed;
    [SerializeField] private float _accelerationDuration = 8f;
    [SerializeField] private TrainMovement _trainMovement;
    [SerializeField] private Button _startTutor;
    [SerializeField] private PlayableDirector _firstCutscene;
    [SerializeField] private PlayableDirector _secondCutscene;

    private float _tutorActivationDelay = 2f;

    private void OnEnable()
    {
        Invoke(nameof(OnActivateTutor), _tutorActivationDelay);
        _startTutor.onClick.AddListener(OnDepartueTrain);
    }

    private void OnDisable()
    {
        _startTutor.onClick.RemoveListener(OnDepartueTrain);
    }

    private void OnActivateTutor()
    {
        _startTutor.gameObject.SetActive(true);
    }

    private void OnDepartueTrain()
    {
        ChangeTimeline();
        StartCoroutine(_trainMovement.ChangeSpeed(_traiMaxSpeed, _accelerationDuration));
        _startTutor.gameObject.SetActive(false);
    }

    private void ChangeTimeline()
    {
        _firstCutscene.Stop();
        _secondCutscene.Play();
    }
}
