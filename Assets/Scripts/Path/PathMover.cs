using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    [SerializeField] private float _criticalOffset;

    private NodeMover[] _nodes;

    public void Initialize()
    {
        _nodes = GetComponentsInChildren<NodeMover>();
    }

    public void MovePath(float inputValue)
    {
        float offset = _criticalOffset * inputValue;
        foreach (var node in _nodes)
        {
            node.SetOffset(offset);
        }
    }
}
