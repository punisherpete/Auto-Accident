using System;
using UnityEngine;

public class HeightChaser : MonoBehaviour
{
    [SerializeField] private float _heightToStartChase;

    public event Action SearchingHeightReached;

    private void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit))
        {
            if (Vector3.Distance(transform.position, hit.point) > _heightToStartChase)
            {
                SearchingHeightReached?.Invoke();
            }
        }
    }
}
