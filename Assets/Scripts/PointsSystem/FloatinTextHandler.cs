using MoreMountains.Feedbacks;
using UnityEngine;

public class FloatinTextHandler : MonoBehaviour
{
    [SerializeField] private MMFeedbacks _floatingTextFeedback;
    [SerializeField] private ScenePointsPool _scenePointsPool;

    private void OnEnable()
    {
        _scenePointsPool.PointsWthidrawed += OnPlayFeedbacks;
    }

    private void OnDisable()
    {
        _scenePointsPool.PointsWthidrawed -= OnPlayFeedbacks;
    }

    private void OnPlayFeedbacks(int textValue)
    {
        _floatingTextFeedback?.PlayFeedbacks(transform.position, textValue);
    }
}
