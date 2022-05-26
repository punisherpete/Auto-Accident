using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer[] _renderers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Mover>())
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                for (int j = 0; j < _renderers[i].materials.Length; j++)
                {
                    _renderers[i].materials[j].color = new Color(1, 1, 1, 0.35f);
                }
            }
        }
    }
}
