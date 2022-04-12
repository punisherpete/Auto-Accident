using UnityEngine;

public class PathMover : MonoBehaviour
{
    private NodeMover[] _nodes;

    public void Initialize()
    {
        _nodes = GetComponentsInChildren<NodeMover>();
    }

    public void MovePath(float offset)
    {
        foreach (var node in _nodes)
        {
            node.SetOffset(offset);
        }
    }
}
