using UnityEngine;

public class BoosterMovemets : MonoBehaviour
{
    private void Update()
    {
        float yCoordinate = Mathf.PingPong(Time.time * 0.5f, 0.3f);
        transform.localPosition = new Vector3(0f, yCoordinate, 0f);
    }
}
