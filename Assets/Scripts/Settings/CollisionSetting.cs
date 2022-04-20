using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSetting : MonoBehaviour
{
    [SerializeField] private bool _reuseCollisinCallbacks;

    private void Start()
    {
        Physics.reuseCollisionCallbacks = _reuseCollisinCallbacks;
    }
}
