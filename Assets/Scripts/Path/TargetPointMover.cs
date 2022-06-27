using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointMover : MonoBehaviour
{
    private Vector3 _startPosition;
    private float _targetOffset;

    public bool IsPointAchiveToTargetOffset => Mathf.RoundToInt(transform.localPosition.x) == Mathf.RoundToInt(_targetOffset);

    private void OnEnable()
    {
        _startPosition = new Vector3(0,0,0);
    }

    public void Move(float criticalOffset, float offsetSpeed, float input)
    {
        if (Vector3.Distance(transform.localPosition, _startPosition) < criticalOffset)
            transform.Translate(transform.right * offsetSpeed * input * Time.deltaTime, Space.World);
        else if (input > 0 && transform.localPosition.x < 0)
            transform.Translate(transform.right * offsetSpeed * input * Time.deltaTime, Space.World);
        else if (input < 0 && transform.localPosition.x > 0)
            transform.Translate(transform.right * offsetSpeed * input * Time.deltaTime, Space.World);
        else if (Vector3.Distance(transform.localPosition, _startPosition) > criticalOffset + 0.5f)
        {
            if (transform.localPosition.x < 0)
                transform.Translate(transform.right * offsetSpeed * Time.deltaTime, Space.World);
            else if (transform.localPosition.x > 0)
                transform.Translate(transform.right * -offsetSpeed * Time.deltaTime, Space.World);
        }
    }

    public void MoveToNewTargetPosition(float offsetSpeed, float targetOffset)
    {
        _targetOffset = targetOffset;
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(targetOffset,0,0), offsetSpeed * Time.deltaTime);
    }
}
