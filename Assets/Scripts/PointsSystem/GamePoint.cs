using System;
using UnityEngine;

public class GamePoint : MonoBehaviour
{
    [SerializeField] private int _value;

    public int Value => _value;

    public event Action<int> BeenCollected;

    [ContextMenu("Collect")]
    public void Collect()
    {
        BeenCollected?.Invoke(Value);
        GetComponent<Collider>().enabled = false;
    }
}
