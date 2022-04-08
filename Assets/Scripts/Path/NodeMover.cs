using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMover : MonoBehaviour
{
    private float _startXPosition;

    private void OnEnable()
    {
        _startXPosition = transform.localPosition.x;
    }

    public void SetOffset(float offset)
    {
        transform.localPosition = new Vector3(_startXPosition - offset, transform.localPosition.y, transform.localPosition.z);
    }
}
