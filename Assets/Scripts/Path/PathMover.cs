using UnityEngine;

public class PathMover : MonoBehaviour
{
    private NodeMover[] _nodes;

    public void Initialize()
    {
        _nodes = GetComponentsInChildren<NodeMover>();
    }

    public void MovePath(float criticalOffset, float offsetSpeed, float input)
    {
        foreach (var node in _nodes)
        {
            node.SetOffset(criticalOffset,offsetSpeed,input);
        }
    }
}
