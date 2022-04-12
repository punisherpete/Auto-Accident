using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    [SerializeField] private WheelCollider _wheelCollider;

    private void Update()
    {
        Quaternion rotation;
        Vector3 worldPosition;
        _wheelCollider.GetWorldPose(out worldPosition, out rotation);
        transform.position = worldPosition;
        transform.rotation = rotation;
    }
}
