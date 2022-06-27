using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    [SerializeField] private Color _pathColor;
    [SerializeField] private float _sphereRadius;

    private List<Transform> _nodes;

    private void OnDrawGizmos()
    {
        Gizmos.color = _pathColor;
        _nodes = new List<Transform>();
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        if (pathTransforms.Length < 2)
            return;
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != transform)
                _nodes.Add(pathTransforms[i]);
        }
        for (int i = 0; i < _nodes.Count; i++)
        {
            Vector3 currentNode = _nodes[i].position;
            Vector3 previouwsNode = _nodes[i].position;
            if (i>0)
                previouwsNode = _nodes[i - 1].position;
            Gizmos.DrawLine(previouwsNode, currentNode);
            Gizmos.DrawWireSphere(currentNode, _sphereRadius);
        }
    }
}
