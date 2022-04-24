using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinstParameters : ScriptableObject
{
    [SerializeField] private Rigidbody _connectedBody;
    [SerializeField] private Vector3 _anchor;
    [SerializeField] private Vector3 _axis;
}
